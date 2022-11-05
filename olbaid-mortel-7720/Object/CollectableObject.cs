namespace olbaid_mortel_7720.Object
{
    /// <summary>
    /// Class for all types of objects that are collectable
    /// </summary>
    internal class CollectableObject : Object
    {
        bool Collectable;
        int Lifetime;

        //Method of asking whether to collect
        public bool Collect()
        {
            return true;
        }
    }
}
