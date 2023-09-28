

namespace Framework.core.comman
{
    public class Pagination
    {
        public int? PageNumber { get; set; }   // for specify the current Page Number
        public int? PageSize { get; set; }    // for specify number of items per page
        public int? TotalCount { get; set; }  // for specify total number of items
        public bool GetTotalCount { get; set; } = true;

    }
}


/*  
    what is information will need it for Pagination?
    
        
   The Pagination class is used to represent pagination information 
   such as 
           the current page number, 
           the number of items per page,
           and the total number of items.
   
   The PageNumber property is used to specify the current page number. The default value is 0.
   
   The PageSize property is used to specify the number of items per page. The default value is 10.
   
   The TotalCount property is used to specify the total number of items. This property is optional and can be set to null.
   
   The GetTotalCount property is used to specify whether or not to retrieve the total count of items. The default value is true.
   
   By using these properties, you can easily implement pagination in your application. For example, you can use the PageIndex and PageSize properties to get the entities for a specific page, and you can use the TotalCount property to display the total number of items.
 
 */