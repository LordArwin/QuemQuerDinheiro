create database qqd;
use qqd;

create table usuario(
idusuario int(11) primary key auto_increment,
login varchar(12) not null,
nome varchar (30) not null,
senha varchar(30) not null
);

create table carteira(
idcarteira int(11) primary key auto_increment,
saldo double(30,2) not null,
idusuario int(11)
);

alter table carteira
add
constraint idusuario_fk_carteira
foreign key (idusuario)
references usuario(idusuario)
on delete cascade
on update cascade;

create table despesa(
iddespesa int(11) primary key auto_increment,
valor double(10,2) not null,
categoria varchar(45) not null,
idcarteira int(11),
data varchar(11) not null,
descricao varchar(30)
);

alter table despesa
add
constraint idcarteira_fk_despesa
foreign key (idcarteira)
references carteira(idcarteira)
on delete cascade
on update cascade;

create table receita(
idreceita int(11) primary key auto_increment,
valor double(10,2) not null,
data varchar(11) not null,
categoria varchar(45) not null,
idcarteira int(11),
descricao varchar(30)
);

alter table receita
add
constraint idcarteira_fk_receita
foreign key (idcarteira)
references carteira(idcarteira)
on delete cascade
on update cascade;


create table meta(
idmeta int(11) primary key auto_increment,
valor double(10,2) not null,
descricao varchar(30),
valor_comprido double(10,2),
idcarteira int(11),
data varchar(8)
);

alter table meta
add
constraint idcarteira_fk_meta
foreign key (idcarteira)
references carteira(idcarteira)
on delete cascade
on update cascade;



