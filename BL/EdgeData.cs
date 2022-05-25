namespace BL
{
    public class EdgeData
    {
        public string FromNode { get; set; }
        public string ToNode { get; set; }
        public RelationType RelationType { get; set; }

        public string Description()
        {
            string result = "Data Unavailable";
            switch (RelationType)
            {
                case RelationType.OneToOne:
                    result = $"Each {FromNode} can have One {ToNode}";
                    break;
                case RelationType.OneToMany:
                    result = $"Each {FromNode} can have Multiple {ToNode}";
                    break;
                case RelationType.ManyToOne:
                    result = $"Each {ToNode} can have Multiple {FromNode}";
                    break;
                case RelationType.MantToMany:
                    result = $"Multiple {FromNode} can have Multiple {ToNode}";
                    break;
                default:
                    break;
            }
            return result;
        }

    }
}