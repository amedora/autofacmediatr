ALTER SESSION SET CONTAINER = localtodo;

DROP TABLE "TODO"."TODOTASKS"; 

CREATE TABLE "TODO"."TODOTASKS" ( 
  "TODO" VARCHAR2(200) NOT NULL
);
