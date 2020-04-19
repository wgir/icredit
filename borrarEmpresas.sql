delete from conceptoaporte where empresaid!=1;
delete from conceptoretiro where empresaid!=1;
delete from consecutivo where empresaid!=1;
delete from usuario where empresaid!=1;
delete from aspnetusers where id in(select aspnetusersId from usuario where empresaId!=1);
delete from cliente where empresaid!=1;
delete from ciudad where empresaid!=1;
delete from empresa where empresaid!=1;
delete from aspnetusers where email!='williamgustavo@gmail.com';

