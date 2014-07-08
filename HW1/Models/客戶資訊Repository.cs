using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HW1.Models
{   
	public  class 客戶資訊Repository : EFRepository<客戶資訊>, I客戶資訊Repository
	{

	}

	public  interface I客戶資訊Repository : IRepository<客戶資訊>
	{

	}
}