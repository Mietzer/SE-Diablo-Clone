namespace olbaid_mortel_7720.Object
{
    /// <summary>
    ///  Class for objects that can be destroyed
    /// </summary>
    internal class DestroyableObject : Object
    {
        bool Destroyable;

        public bool IsDestroy()
        {
            return true;
        }
    }
}
