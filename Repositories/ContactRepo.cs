using SliceMasterBE.Data;
using Microsoft.EntityFrameworkCore;
using SliceMasterBE.Models;
namespace SliceMasterBE.Repositories
{
	public class ContactRepo : IContactRepo
	{
		private readonly SliceMasterDB _DB;
		public ContactRepo(SliceMasterDB DB)
		{
			_DB = DB;
		}
		public async Task<List<ContactMsg>> GetAllContactMsgs()
		{
			var list = await _DB.Contact.ToListAsync();
			return list;

		}
		public async Task<ContactMsg> GetContactMsg(int id)
		{
			var Msg = await _DB.Contact.FindAsync(id);
			return Msg;
			

		}
		public async Task CreateContactMsg(ContactMsgModel msgModel)
		{
			ContactMsg msg = new ContactMsg() { 
			Name = msgModel.Name,
			Email = msgModel.Email,
			Message = msgModel.Message,
			};
			_DB.Contact.Add(msg);
			await _DB.SaveChangesAsync();
		}
		public async Task<string?> DeleteContactMsg(int id)
		{
			if (await GetContactMsg(id)==null) {
				return null;
			}
			ContactMsg msg = new ContactMsg() { 
			Id =id
			};
			_DB.Contact.Remove(msg);
			await _DB.SaveChangesAsync();
			return "deleted";
		}


	}
}
