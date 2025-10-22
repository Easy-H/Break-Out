
namespace ObjectPool
{

    public interface IPoolTarget
    {
        public void SetParentPool(Pool parent);
        public void Return();
    }
    
}