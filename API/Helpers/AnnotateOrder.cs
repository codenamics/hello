using System.Collections.Generic;
using API.Entity;
using API.Interfaces;

namespace API.Helpers {
    public interface IOrder {
        int Order { get; set; }
       
    }
    
    public class AnnotateOrder<T> : List<T> where T : IOrder {

         public List<T> AnnotatedOrder (List<T> ent) {
            if (ent != null) {
                for (int i = 0; i < ent.Count; i++) {
                    ent[i].Order = i;
                }
            }
            return ent;
        }
    }
}