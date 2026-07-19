namespace ForgeCore.Inventories.Tests.Domain;

public class InventoryTest
{
    [Fact]
    public void Create_Should_Assign_Id()
    {
        // Arrange
        var inventory = InventoryFactory.Create();

        // Assert 
        inventory.Id.Should().NotBeEmpty("because the constructor generates a random guid");
    }

    [Fact]
    public void Create_Should_Assign_Owner_Id()
    {
        // Arrange
        var inventory = InventoryFactory.Create();

        // Assert 
        inventory.OwnerId.Should().NotBeEmpty("because the constructor must set the stablished owner id");
    }

    [Fact]
    public void AddEntry_Should_Add_New_Entry()
    {
        // Arrange
        var inventory = InventoryFactory.Create();
        var entry = new InventoryEntry(itemKey: "golden-apple", quantity: 2, slotIndex: null, isStackable: true);

        // Act
        inventory.AddEntry(entry);

        // Assert 
        inventory.Entries.FirstOrDefault(e => e.ItemKey == "golden-apple").Should().NotBeNull("because the add entry function must add to the list the new entry");
    }

    [Fact]
    public void AddEntry_Should_Increase_Quantity_When_Item_Is_Stackable()
    {
        // Arrange
        var inventory = InventoryFactory.Create();
        var entry = new InventoryEntry(itemKey: "golden-apple", quantity: 1, slotIndex: null, isStackable: true);
        inventory.AddEntry(entry);

        // Act
        inventory.AddEntry(entry);

        // Assert
        inventory.Entries.First(e => e.ItemKey == "golden-apple").Quantity.Should().Be(2, "because the add entry function should find existing entries by item key, and if it is stackable add one entry to the registry");
    }

    [Fact]
    public void AddEntry_Should_Not_Merge_NonStackable_Items()
    {
        // Arrange
        var inventory = InventoryFactory.Create();
        var entry = new InventoryEntry(itemKey: "pickaxe", quantity: 1, slotIndex: null, isStackable: false);
        inventory.AddEntry(entry);

        // Act
        inventory.AddEntry(entry);

        // Assert
        inventory.Entries.Where(e => e.ItemKey == "pickaxe").ToList().Count.Should().Be(2);
    }

    [Fact]
    public void RemoveEntry_Should_Remove_An_Entry()
    {
        // Arrange
        var inventory = InventoryFactory.Create();
        var entry = new InventoryEntry(itemKey: "golden-apple", quantity: 2, slotIndex: null, isStackable: true);

        inventory.AddEntry(entry);

        // Act
        inventory.RemoveEntry(entry.Id);

        // Assert
        inventory.Entries.Should().NotContain(e => e.Id == entry.Id, "because the remove entry function searches the entry by id and removes it from the collection");
    }

    [Fact]
    public void RemoveEntry_Should_Throw_When_Entry_Does_Not_Exist()
    {
        // Arrange
        var inventory = InventoryFactory.Create();
        var entryGuid = Guid.NewGuid();

        // Act
        Action act = () => inventory.RemoveEntry(entryGuid);

        // Assert
        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage($"*Entry {entryGuid} not found.*");
    }

    [Fact]
    public void ModifyEntry_Should_Update_Entry()
    {
        // Arrange 
        var inventory = InventoryFactory.Create();
        var entry = new InventoryEntry(itemKey: "golden-apple", quantity: 2, slotIndex: null, isStackable: true);

        inventory.AddEntry(entry);

        // Act
        var newEntry = new InventoryEntry(itemKey: "golden-apple", quantity: 1, slotIndex: null, isStackable: false);
        inventory.ModifyEntry(oldEntryId: entry.Id, newEntry: newEntry);

        // Assert
        var modifiedEntry = inventory.Entries.First(e => e.ItemKey == "golden-apple");
        modifiedEntry.Quantity.Should().Be(1, "because the modify entry function searches the entry by id and replaces the values with the new ones");
        modifiedEntry.IsStackable.Should().Be(false, "because the modify entry function searches the entry by id and replaces the values with the new ones");
    }

    [Fact]
    public void ModifyEntry_Should_Throw_When_Entry_Does_Not_Exist()
    {
        // Arrange
        var inventory = InventoryFactory.Create();
        var entry = new InventoryEntry(itemKey: "golden-apple", quantity: 2, slotIndex: null, isStackable: true);
        var entryGuid = Guid.NewGuid();

        // Act
        Action act = () => inventory.ModifyEntry(oldEntryId: entryGuid, newEntry: entry);

        // Assert
        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage($"*Entry {entryGuid} not found.*");
    }

    [Fact]
    public void ClearEntries_Should_Remove_All_Entries()
    {
        // Arrange
        var inventory = InventoryFactory.Create();
        var entry = new InventoryEntry(itemKey: "golden-apple", quantity: 2, slotIndex: null, isStackable: true);
        var entryGuid = Guid.NewGuid();

        // Act
        inventory.ClearEntries();

        // Assert
        inventory.Entries.Count.Should().Be(0, "because the clear entries function must call the clear list function");
    }
}
