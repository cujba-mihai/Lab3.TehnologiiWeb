﻿namespace Lab3.TehnologiiWeb
{
    public class Grupa
    {
        public int ID { get; set; }
        public string Denumire { get; set; }

        public override string ToString()
        {
            return $"{Denumire}";
        }
    }
}
