using UserManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Interface;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
