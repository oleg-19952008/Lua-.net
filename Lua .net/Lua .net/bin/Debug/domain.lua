function dom()
echo("TEST MODE !!!")
-- netdom join tst-001 /Domain:"<имя домена>" 
-- /UserD:"<имя администратора домена>"
 -- /PasswordD:<пароль администратора домена> 
 -- /UserO:"<имя администратора станции>"
 -- /PasswordO:<пароль администратора станции>
 echo("Input name domain\n")
local domain_ = read_() 
echo("Input name pc\n")
local name_pc = read_()
 
 local cmd = "netdom join " .. domain_ .." " ..name_pc ;
echo(cmd);
 end
 --run("")