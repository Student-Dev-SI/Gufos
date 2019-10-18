/* Dados de Tipode usuario*/
INSERT INTO tipoUsuario (Titulo) 
VALUES                  ('Administrador'), 
						('Aluno')


/* Dados de usuario*/
INSERT INTO usuario (Nome, Email, Senha, IDTipoUsuario) 
VALUES              ('Administrador', 'adm@adm.com', '123', 1),
                    ('Ariel','ariel@aluno.com', 123, 2)

/* Dados de localização*/
INSERT INTO localizacao (CNPJ, RazãoSocial, Endereco) 
VALUES              (03795071000116, 'SERVIÇO NACIONAL DE APRENDIZAGEM INDUSTRIAL - SENAI', 'Al. Barão de Limeira, 539');
              
/* Dados de categoria*/
INSERT INTO categoria (Titulo) 
VALUES              ('Desenvolvimento'),
                    ('HTML + CSS'),
                    ('Markentig');

/* GETDATE Pega a data atual*/
INSERT INTO evento (Titulo, DataDoEvento, AcessoLivre, IDCategoria, IDLocalizacao)
VALUES              ('C#', '2019-08-07T18:00:00', 2, 0, 1)
                    /*('Estrutura Semântica',  GETDATE(), 2, 1, 1)*/

/*Pega a data atual*/
INSERT INTO presenca (PresencaStatus, IDUsuario, IDEvento) 
VALUES              ( 'Aguardando',  1, 40),   
                    ( 'Confirmação', 1, 40)
