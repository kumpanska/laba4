using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public static class FundsMapper
    {
        public static Funds FromDTO(DTOFunds dto)
        {
            return new Funds(dto.NameOfFund, dto.Address);
        }
        public static DTOFunds ToDTO(Funds funds)
        {
            return new DTOFunds
            {
                NameOfFund = funds.Name,
                Address = funds.Address
            };
        }
    }
}
