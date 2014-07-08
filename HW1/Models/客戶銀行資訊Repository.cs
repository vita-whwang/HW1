using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HW1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{

        public IQueryable<客戶銀行資訊> GetAll()
        {
            return All().Where(p => p.isDelete == false);
        }

        public 客戶銀行資訊 FindBankById(int id)
        {
            return Where(p => p.Id == id).FirstOrDefault();
        }
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}