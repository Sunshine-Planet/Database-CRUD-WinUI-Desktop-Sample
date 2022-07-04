
SHOW DATABASES;
DROP DATABASE IF EXISTS trafficpunishment;
CREATE DATABASE trafficpunishment;
USE trafficpunishment;
DROP TABLE IF EXISTS driver;
CREATE TABLE driver(
    driver_id VARCHAR(18) NOT NULL   COMMENT '身份证号' ,
    driver_name VARCHAR(10) NOT NULL   COMMENT '姓名' ,
    driver_gender VARCHAR(10) NOT NULL DEFAULT '男' COMMENT '性别' ,
    driver_age INT NOT NULL   COMMENT '年龄' ,
    driver_post VARCHAR(12) NOT NULL   COMMENT '驾驶证编号' ,
    driver_phone VARCHAR(11) NOT NULL   COMMENT '电话号码' ,
    driver_address VARCHAR(20) NOT NULL   COMMENT '居住地址' ,
    driver_zip VARCHAR(6) NOT NULL   COMMENT '邮政编号' ,
    PRIMARY KEY (driver_id)
)  COMMENT = '驾驶人';

DROP TABLE IF EXISTS car;
CREATE TABLE car(
    car_mastername VARCHAR(10) NOT NULL COMMENT '所有者姓名' ,
    car_masterid VARCHAR(18) NOT NULL COMMENT '所有者身份证号' ,
    car_id VARCHAR(20) NOT NULL   COMMENT '车牌号' ,
    car_prouducer VARCHAR(60) NOT NULL   COMMENT '生产厂家' ,
    car_mode VARCHAR(20) NOT NULL   COMMENT '型号' ,
    car_landdate DATE NOT NULL   COMMENT '出厂日期' ,
    car_boughtdate DATE NOT NULL   COMMENT '购买日期' ,
    car_boughtlocation VARCHAR(60) NOT NULL   COMMENT '购买地点' ,
    PRIMARY KEY (car_id)
)  COMMENT = '机动车';

DROP TABLE IF EXISTS punishment;
CREATE TABLE punishment(
    punishment_mastername VARCHAR(20) NOT NULL   COMMENT '被执行人姓名' ,
    punishment_masterid VARCHAR(18) NOT NULL   COMMENT '被执行人身份证号' ,
    punishment_number VARCHAR(20) NOT NULL   COMMENT '处理编号' ,
    punishment_officer VARCHAR(50) NOT NULL   COMMENT '执行交警' ,
    punishment_date DATE NOT NULL   COMMENT '违章日期' ,
    punishment_time TiME NOT NULL   COMMENT '违章时间' ,
    punishment_location VARCHAR(60) NOT NULL   COMMENT '违章地点' ,
    punishment_reason VARCHAR(60) NOT NULL   COMMENT '违章项目' ,
    punishment_method CHAR(1) NOT NULL   COMMENT '处罚方式' ,
    punishment_sign VARCHAR(20) NOT NULL    COMMENT '被处罚人签字' ,
    PRIMARY KEY (punishment_number)
)  COMMENT = '违章信息';

DROP TABLE IF EXISTS officer;
CREATE TABLE officer(
    officer_name VARCHAR(20) NOT NULL   COMMENT '交警姓名' ,
    officer_id CHAR(3) NOT NULL   COMMENT '交警编号' ,
    PRIMARY KEY (officer_id)
)  COMMENT = '交警队';


CREATE OR REPLACE VIEW Ticket
(`编号`, `违章人`,  `驾驶执照号`, `地址`, `邮编`, `电话`, `机动车牌照号`, `型号`, `制造商`, `生产日期`, `违章日期`, `违章时间`, `违章地点`, `违章记载`, `处罚方式`, `处理交警`, `交警编号`, `被处罚人签字`)
AS
SELECT punishment.punishment_number, punishment.punishment_mastername, driver.driver_post, driver.driver_address, driver.driver_zip, driver.driver_phone, car.car_id, car.car_mode, car.car_prouducer, car.car_landdate, punishment.punishment_date, punishment.punishment_time, punishment.punishment_location, punishment.punishment_reason, punishment.punishment_method, officer.officer_name, officer.officer_id, punishment.punishment_sign
FROM punishment, driver, car, officer
WHERE punishment.punishment_masterid=driver.driver_id AND driver.driver_id=car.car_masterid AND punishment.punishment_officer=officer.officer_name;

INSERT INTO driver 
(driver_id, driver_name, driver_gender, driver_age, driver_post, driver_phone, driver_address, driver_zip)
VALUES
("360429xxxxxxxx1111", "张三", "男", "18", "123456789012", "11111111111", "江西九江", "332000"),
("360429xxxxxxxx1112", "麻子", "女", "19", "123456789013", "11111111112", "浙江杭州", "310000"),
("360429xxxxxxxx1113", "王二", "男", "33", "123456789014", "11111111113", "上海徐家汇", "200030"),
("360429xxxxxxxx1114", "佚名", "男", "28", "123456789015", "11111111114", "湖北武汉", "430000");
#SELECT * FROM  driver;


INSERT INTO car
(car_mastername, car_masterid ,car_id, car_prouducer, car_mode, car_landdate, car_boughtdate, car_boughtlocation)
VALUES
("张三", "360429xxxxxxxx1111","赣G12345", "蔚来", "越野型", "1970-01-01", "1970-01-01", "江西九江"),
("麻子", "360429xxxxxxxx1112","浙A12345", "雪佛兰", "城市型", "1970-01-01", "1970-01-01", "浙江杭州"),
("王二", "360429xxxxxxxx1113","沪B12345", "吉利", "山地型", "1970-01-01", "1970-01-01", "上海徐家汇"),
("佚名", "360429xxxxxxxx1114","鄂A12345", "大众", "城市型", "1970-01-01", "1970-01-01", "湖北武汉");
#SELECT * FROM car;

INSERT INTO punishment
(punishment_mastername, punishment_masterid, punishment_number, punishment_officer, punishment_date, punishment_time, punishment_location, punishment_reason, punishment_method, punishment_sign)
VALUES
("张三", "360429xxxxxxxx1111", "TZ00000", "李四", "2021-01-01", "12:10:00","江西九江", "闯红灯", "4", "张三"),
("张三", "360429xxxxxxxx1111", "TZ00001", "李四", "2021-06-02", "14:20:00","江西九江", "未在指定位置停车", "4", "张三"),
("张三", "360429xxxxxxxx1111", "TZ00002", "建国", "2021-06-03", "16:50:00","江西九江", "交通肇事后逃逸", "6", "张三"),
("佚名", "360429xxxxxxxx1114", "TZ00003", "李四", "2021-09-04", "20:10:00", "上海徐家汇", "酒驾", "7", "佚名"),
("佚名", "360429xxxxxxxx1114", "TZ00005", "建国", "2021-09-15", "12:10:00","江西九江", "闯红灯", "4", "佚名");
#SELECT * FROM punishment;

INSERT INTO officer
(officer_name, officer_id)
VALUES
("李四", "995"),
("建国", "997");
#SELECT * FROM officer;



SELECT * FROM Ticket;

SELECT * FROM Ticket
WHERE 违章日期 BETWEEN "2021-06-01" AND "2021-08-31";

SELECT * FROM Ticket
WHERE 违章人 = "张三";


SELECT * FROM Ticket
WHERE 机动车牌照号 = "鄂A12345";

SELECT * FROM Ticket
WHERE 处理交警 = "李四";


