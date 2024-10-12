using SliceMasterBE.Data;
using SliceMasterBE.Models;

namespace SliceMasterBE.Repositories
{
	public interface IContactRepo
	{
		Task<List<ContactMsg>> GetAllContactMsgs();
		Task<ContactMsg> GetContactMsg(int id);
		Task CreateContactMsg(ContactMsgModel msgModel);
		Task<string?> DeleteContactMsg(int id);
	}
}