namespace olbaid_mortel_7720.Object
{
    /// <summary>
    ///  Basic Class for all types of Object 
    /// </summary>
    internal class Object : Graphics
    {
        public string PathtoGraphics { get; set; }
        public int Lifepoints { get; set; }
        bool Visible;
        public bool Penetrable { get; set; }
    }
}
