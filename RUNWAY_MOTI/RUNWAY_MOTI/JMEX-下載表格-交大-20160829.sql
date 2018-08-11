/* Country 資料 */
CREATE TABLE country_info ( 
	numeric_code         varchar(3)  NOT NULL  ,
	country_name         varchar(128)  NOT NULL  ,
	alpha3_code          varchar(3)    ,
	nationality          varchar(128)  NOT NULL  ,
	CONSTRAINT pk_country_info PRIMARY KEY ( numeric_code )
 );

/* Group 資料 */
 CREATE TABLE group_info ( 
	group_id             int  NOT NULL  ,
	group_name           varchar(100)    ,
	CONSTRAINT pk_group_info PRIMARY KEY ( group_id )
 );

/* 會員資料 */
CREATE TABLE member_info ( 
	member_id            varchar(50)  NOT NULL  ,			/* 會員編號 */
	member_fname         varchar(128)  ,					/* 會員名字 */
	member_lname         varchar(128)    ,					/* 會員姓氏 */
	email                varchar(128)  NOT NULL  ,			/* 會員的 e-mail 帳號 */
	password             varchar(32)  NOT NULL  ,			/* 密碼 */
	fb_email             varchar(128)    ,					/* 若是使用 FB 帳號登入者，此欄位與email欄位值相同 */
	twitter_email        varchar(128)    ,					/* 目前沒有使用 */
	gender               tinyint  NOT NULL DEFAULT 1 ,		/* 性別 */
	height               decimal(6,2)    ,					/* 身高 */
	weight               decimal(6,2)    ,					/* 體重 */
	numeric_code         varchar(3)    , 					/* 目前沒有使用 */
	is_coach             bool  NOT NULL DEFAULT 0 ,			/* 是否為教練 */
	years_of_exp         decimal(4,1)    ,					/* 教練的經驗年數 */
	personal_intro       text    ,							/* 個人簡介 */
	profile_photo        varchar(1024)    ,					/* 個人照片 */
	unit_length          tinyint  NOT NULL DEFAULT 0 ,		/* 身高的單位 */
	unit_weight          tinyint  NOT NULL DEFAULT 0 ,		/* 體重的單位 */
	unit_distance        tinyint  NOT NULL DEFAULT 0 ,		/* 距離的單位 */
	social_connect_fb    bool  NOT NULL DEFAULT 0 ,			/* 帳號是否有連接 FB */
	social_connect_twitter bool  NOT NULL DEFAULT 0 ,		/* 帳號是否有連接 Twitter */
	app_version          varchar(32)    ,					/* MOTi APP 的版本 */
	stride               int  NOT NULL DEFAULT 30 ,			/* 會員的步距 */
	fitness_load_setting int  NOT NULL DEFAULT 10 ,			/* 會員健身的 default 負重 */
	group_id             int    ,							/* 會館的 ID */
	action_flag          tinyint    ,						/* 資料上傳是否有成功，0:未上傳 1:已上傳 */
	dateofbirth          varchar(8)    ,					/* 會員出生年月日 */
	default_time_offset  decimal(9,2),						/* 會員所屬的時區 */
	CONSTRAINT pk_member_info PRIMARY KEY ( member_id ),
	CONSTRAINT fk_member_info_country_info FOREIGN KEY ( numeric_code ) REFERENCES country_info( numeric_code ) ON DELETE SET NULL ON UPDATE SET NULL,
	CONSTRAINT fk_member_info_group_info FOREIGN KEY ( group_id ) REFERENCES group_info( group_id ) ON DELETE NO ACTION ON UPDATE CASCADE
 );


/* Member_Info 表格欄位值的對應								*/
/* gender      				-> 0:female 1:male 				*/
/* unit_length 				-> 0:cm 1:inch 					*/
/* unit_weight 				-> 0:kg 1:pound 				*/
/* unit_distance 			-> 0:km 1:mile 					*/
/* social_connect_fb 		-> 0: not connect 1: connected 	*/
/* social_connect_twitter 	-> 0: not connect 1: connected 	*/
/* stride 					-> default: 30cm 				*/
/* fitness_load_setting 	-> default:10kg 				*/

/* 會員每週的身體資料記錄，例如:體重，身高，體脂等等 */
CREATE TABLE member_info_weekly_record ( 
	member_id            varchar(50)  NOT NULL  ,			/* 會員編號 */
	record_date          date  NOT NULL  ,					/* 記錄的日期與時間 */
	body_weight          decimal(6,2)  NOT NULL  ,			/* 體重 */
	body_fat             decimal(4,1)    ,					/* 體脂肪 */
	body_guid            varchar(50)    ,					/* 此欄位為與管理系統對接的 ID */
	action_flag          tinyint    ,						/* 資料上傳是否有成功，0:未上傳 1:已上傳 */
	body_bmi             decimal(4,1)    ,					/* BMI:身體質量指數 */
	body_bmr             decimal(6,1)    ,					/* BMR:基礎代謝率 */
	body_water           decimal(4,1)    ,					/* 體內水分 */
	bone_mass            decimal(4,1)    ,					/* 骨量 */
	muscle_rate          decimal(4,1)    ,					/* 肌肉率 */
	body_age             smallint    ,						/* 體內年齡 */
	visceral_fat         decimal(4,1),						/* 內臟脂肪 */
	time_offset          decimal(9,2),						/* 量測資料時，所屬的時區 */
	CONSTRAINT fk_member_info_weekly_record FOREIGN KEY ( member_id ) REFERENCES member_info( member_id ) ON DELETE CASCADE ON UPDATE CASCADE
 ) ; 
 
CREATE INDEX idx_member_info_weekly_record ON member_info_weekly_record ( member_id );

/* 會員的計步運動記錄 */
CREATE TABLE member_pedometer_record ( 
	pedometer_sdatetime  datetime  NOT NULL  ,				/* 走路開始的時間 */
	member_id            varchar(50)  NOT NULL  ,			/* 會員編號 */
	pedometer_edatetime  datetime    ,						/* 走路結束的時間 */
	steps                int    ,							/* 步數 */
	pedometer_guid       varchar(50)    ,					/* 此欄位為與管理系統對接的 ID */
	action_flag          tinyint    ,						/* 資料上傳是否有成功，0:未上傳 1:已上傳 */
	time_offset          decimal(9,2)    ,					/* 運動時，所屬的時區 */
	CONSTRAINT idx_member_pedometer_record PRIMARY KEY ( pedometer_sdatetime, member_id ),
	CONSTRAINT fk_member_pedometer_record FOREIGN KEY ( member_id ) REFERENCES member_info( member_id ) ON DELETE CASCADE ON UPDATE CASCADE
 );

CREATE INDEX idx_member_pedometer_record_0 ON member_pedometer_record ( member_id );

/* J-MEX 可以偵測的健身項目，包含: workout & 循環訓練 */
CREATE TABLE workout_info ( 
	workout_id           smallint  NOT NULL  ,				/* 健身項目的編號 */
	workout_name         varchar(128)  NOT NULL  ,			/* 健身項目名稱 */
	workout_detail       varchar(128)    ,					/* 健身項目的說明 */
	description          text,								/* 健身項目的說明 */
	step		     text,									/* 此健身項目運動時的步驟 */
	tips		     text,									/* 此健身項目運動時的技巧 */
	position             tinyint  NOT NULL DEFAULT 1 ,		/* 運動此健身項目時，使用 MOTi 時穿戴的位置 */
	use_flag	     bool NOT NULL DEFAULT 0,				/* 此健身項目目前是否有提供 */
	workout_prev_image   varchar(1024)    ,					/* 此健身項目的圖片 */
	fitness_type         tinyint    ,						/* 此健身項目屬於重量訓練，還是循環訓練 */
	youtubelink          varchar(1024)    ,					/* 此健身項目於 youtube 的連結 */
	update_stamp         timestamp    ,						/* 資料更新的時間 */
	CONSTRAINT pk_workout_info PRIMARY KEY ( workout_id )
 );

 /* Workout_Info 表格欄位值的對應							*/
/* position     			-> 1:Wrist 2:Upper Arm 3:Ankle 	*/
/* fitness_type 			-> 1:重量訓練 2:循環運動 		*/


/* 會員重量訓練的記錄 */
CREATE TABLE member_fitness_record ( 
	fitness_sdatetime    datetime  NOT NULL  ,				/* 健身開始的時間 */
	member_id            varchar(50)  NOT NULL  ,			/* 會員編號 */
	workout_id           smallint  NOT NULL  ,				/* workout 編號 */
	fitness_edatetime    datetime  NOT NULL  ,				/* 健身結束的時間 */
	fitness_load         decimal(4,1)    ,					/* 負重 */
	fitness_reps         smallint    ,						/* 次數 */
	fitness_guid         varchar(50)    ,					/* 此欄位為與管理系統對接的 ID */
	action_flag          tinyint    ,						/* 資料上傳是否有成功，0:未上傳 1:已上傳 */
	time_offset          decimal(9,2)    ,					/* 運動時，所屬的時區 */
	CONSTRAINT idx_member_fitness_record PRIMARY KEY ( fitness_sdatetime, member_id, workout_id ),
	CONSTRAINT fk_member_fitness_record_1 FOREIGN KEY ( member_id ) REFERENCES member_info( member_id ) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT fk_member_fitness_record_2 FOREIGN KEY ( workout_id ) REFERENCES workout_info( workout_id ) ON DELETE CASCADE ON UPDATE CASCADE
 );

CREATE INDEX idx_member_fitness_record_0 ON member_fitness_record ( member_id );
CREATE INDEX idx_member_fitness_record_1 ON member_fitness_record ( workout_id );

/* 會員做循環訓練的記錄 */
CREATE TABLE member_aerobic_record ( 
	aerobic_sdatetime    datetime  NOT NULL  ,				/* 循環訓練開始的時間 */
	member_id            varchar(50)  NOT NULL  ,			/* 會員編號 */
	workout_id           smallint  NOT NULL  ,				/* workout 編號 */
	aerobic_edatetime    datetime  NOT NULL  ,				/* 循環訓練結束的時間 */
	aerobic_total_reps   smallint    ,						/* 循環訓練此次運動的次數 */
	aerobic_guid         varchar(50)    ,					/* 此欄位為與管理系統對接的 ID */
	action_flag          tinyint    ,						/* 資料上傳是否有成功，0:未上傳 1:已上傳 */
	time_offset          decimal(9,2)    ,					/* 運動時，所屬的時區 */
	CONSTRAINT idx_member_aerobic_record PRIMARY KEY ( aerobic_sdatetime, member_id, workout_id ),
	CONSTRAINT fk_member_aerobic_record FOREIGN KEY ( member_id ) REFERENCES member_info( member_id ) ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT fk_member_aerobic_record_1 FOREIGN KEY ( workout_id ) REFERENCES workout_info( workout_id ) ON DELETE NO ACTION ON UPDATE CASCADE
 );

CREATE INDEX idx_member_aerobic_record_0 ON member_aerobic_record ( member_id );
CREATE INDEX idx_member_aerobic_record_1 ON member_aerobic_record ( workout_id );

