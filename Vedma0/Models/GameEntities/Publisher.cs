namespace Vedma0.Models.GameEntities
{
    public class Publisher:Preset
    {
        public string Adress { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// No root, m_*** - image medium
        /// s_*** - image small
        /// </summary>
        public string Image { get; set; }
        public bool AllowAnonymus { get; set; }


    }
}