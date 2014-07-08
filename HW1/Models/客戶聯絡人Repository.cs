using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HW1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{

        public IQueryable<客戶聯絡人> GetAll()
        {
            return All().Where(p => p.isDelete == false);
        }

        public 客戶聯絡人 FindContactById(int id)
        {
            return Where(p => p.Id == id).FirstOrDefault();
        }

	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}