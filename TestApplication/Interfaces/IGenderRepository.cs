using System;
using TestApplication.Models;

namespace TestApplication.Interfaces
{
	public interface IGenderRepository
	{
        Gender GetGenderByKey(int genderKey);
    }
}

