using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HW1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public IQueryable<客戶資料> GetAll()
        {
            return All().Where(p => p.isDelete == false);
        }

        public 客戶資料 FindClientById(int id)
        {
            return Where(p => p.Id == id).FirstOrDefault();
        }
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}