DELIMITER $$
CREATE PROCEDURE adicionar_usuario (IN param_login VARCHAR(12) , in param_nome varchar(30) , in param_senha varchar(30))
BEGIN
    insert into usuario values(default,param_login,param_nome,param_senha); 
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE adicionar_despesa (IN param_valor double(10,2) , in param_categoria varchar(45) ,
 in param_id_carteira int(11), in param_data varchar(11), in param_descricao varchar(30)
)
BEGIN
    insert into despesa values(default,param_valor,param_categoria,param_id_carteira,param_data,param_descricao); 
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE adicionar_ganho (IN param_valor double(10,2), in param_data varchar(11) , in param_categoria varchar(45) ,
 in param_id_carteira int(11), in param_descricao varchar(30)
)
BEGIN
    insert into receita values(default,param_valor,param_data,param_categoria,param_id_carteira,param_descricao); 
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE adicionar_meta (IN param_valor double(10,2), in param_descricao varchar(30), in param_cumprido double(10,2) ,
 in param_id_carteira int(11), in param_data varchar(8) 
)
BEGIN
    insert into meta values(default,param_valor,param_descricao,param_cumprido,param_id_carteira,param_data); 
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE remover_meta (IN param_id_carteira int(11), IN param_data varchar(8)
)
BEGIN
    delete from meta where idcarteira = param_id_carteira and data = param_data;
END $$
DELIMITER ;




select * from meta;