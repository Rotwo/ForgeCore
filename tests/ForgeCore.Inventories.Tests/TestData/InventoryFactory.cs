namespace ForgeCore.Inventories.Tests.TestData
{
    public static class InventoryFactory
    {
        public static Inventory Create()
        {
            return new Inventory(Guid.NewGuid());   
        }
    }
}
