DROP TABLE IF EXISTS _USER;
CREATE TABLE _USER (
  id              SERIAL PRIMARY KEY,
  first_name           VARCHAR(100) NOT NULL,
  last_name  VARCHAR(100) NULL,
  email  VARCHAR(100) NULL,
  idiom_id int REFERENCES IDIOM(ID)
);

DROP TABLE IF EXISTS IDIOM;
CREATE TABLE IDIOM (
  id              SERIAL PRIMARY KEY,
  description     VARCHAR(100) NOT NULL
);

DROP TABLE IF EXISTS _GROUP;
CREATE TABLE _GROUP (
  id              SERIAL PRIMARY KEY,
  description     VARCHAR(100) NOT NULL
);

DROP TABLE IF EXISTS PERMISSION;
CREATE TABLE PERMISSION (
  id              SERIAL PRIMARY KEY,
  description     VARCHAR(100) NOT NULL
);

DROP TABLE IF EXISTS GROUP_PERMISSION;
CREATE TABLE GROUP_PERMISSION (
  group_id int REFERENCES _GROUP(ID),
  permission_id int REFERENCES PERMISSION(ID)
);