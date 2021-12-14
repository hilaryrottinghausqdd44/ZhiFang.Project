

----------------------输血前(开始)----------------------

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1100,4712222272559814781,N'temperature',N'体温',N'体温',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"°C","ItemDataSet":""}',10,1,N'2020-02-14 14:54:56');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1200) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1200,4712222272559814781,N'bloodpressure',N'血压',N'血压',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}',20,1,N'2020-02-14 15:22:22');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1300)
INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1300,4712222272559814781,N'pulse',N'脉博',N'脉博',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"/min","ItemDataSet":""}',30,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1400)
INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1400,4712222272559814781,N'other',N'患者一般情况',N'患者一般情况',N'{"ItemXType":"uxSimpleComboBox","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":"[{''清醒'':''清醒''},{''昏迷'':''昏迷''},{''嗜睡'':''嗜睡''},{''麻醉'':''麻醉''}]"}',40,1,N'2020-02-14 15:29:11');

----------------------输血前(结束)----------------------

----------------------输血15分钟(开始)-------------------
IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2100,5510188224824882839,N'temperature',N'体温',N'体温',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"°C","ItemDataSet":""}',10,1,N'2020-02-14 14:54:56');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2200)
INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2200,5510188224824882839,N'bloodpressure',N'血压',N'血压',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}',20,1,N'2020-02-14 15:22:22');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2300,5510188224824882839,N'pulse',N'脉博',N'脉博',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"/min","ItemDataSet":""}',30,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2400,5510188224824882839,N'pulse',N'滴速',N'滴速',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"滴/分","ItemDataSet":""}',40,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2500,5510188224824882839,N'other',N'患者一般情况',N'患者一般情况',N'{"ItemXType":"uxSimpleComboBox","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":"[{''正常'':''正常''},{''异常'':''异常''}]"}',50,1,N'2020-02-14 15:29:11');

----------------------输血15分钟(结束)-------------------


----------------------输血60分钟(开始)-------------------
IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3100,5109775278160421817,N'temperature',N'体温',N'体温',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"°C","ItemDataSet":""}',10,1,N'2020-02-14 14:54:56');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3200)
INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3200,5109775278160421817,N'bloodpressure',N'血压',N'血压',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}',20,1,N'2020-02-14 15:22:22');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3300,5109775278160421817,N'pulse',N'脉博',N'脉博',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"/min","ItemDataSet":""}',30,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3400,5109775278160421817,N'pulse',N'滴速',N'滴速',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"滴/分","ItemDataSet":""}',40,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3500,5109775278160421817,N'other',N'患者一般情况',N'患者一般情况',N'{"ItemXType":"uxSimpleComboBox","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":"[{''正常'':''正常''},{''异常'':''异常''}]"}',50,1,N'2020-02-14 15:29:11');

----------------------输血60分钟(结束)-------------------


----------------------输血输血2小时(开始)-------------------
IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4100,5660775821937029735,N'temperature',N'体温',N'体温',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"°C","ItemDataSet":""}',10,1,N'2020-02-14 14:54:56');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4200)
INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4200,5660775821937029735,N'bloodpressure',N'血压',N'血压',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}',20,1,N'2020-02-14 15:22:22');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4300,5660775821937029735,N'pulse',N'脉博',N'脉博',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"/min","ItemDataSet":""}',30,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4400,5660775821937029735,N'pulse',N'滴速',N'滴速',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"滴/分","ItemDataSet":""}',40,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4500,5660775821937029735,N'other',N'患者一般情况',N'患者一般情况',N'{"ItemXType":"uxSimpleComboBox","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":"[{''正常'':''正常''},{''异常'':''异常''}]"}',50,1,N'2020-02-14 15:29:11');

----------------------输血输血2小时(结束)-------------------


----------------------输血输血3小时(开始)-------------------
IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5100,4959979330910815717,N'temperature',N'体温',N'体温',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"°C","ItemDataSet":""}',10,1,N'2020-02-14 14:54:56');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5200)
INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5200,4959979330910815717,N'bloodpressure',N'血压',N'血压',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}',20,1,N'2020-02-14 15:22:22');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5300,4959979330910815717,N'pulse',N'脉博',N'脉博',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"/min","ItemDataSet":""}',30,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5400,4959979330910815717,N'pulse',N'滴速',N'滴速',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"滴/分","ItemDataSet":""}',40,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5500,4959979330910815717,N'other',N'患者一般情况',N'患者一般情况',N'{"ItemXType":"uxSimpleComboBox","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":"[{''正常'':''正常''},{''异常'':''异常''}]"}',50,1,N'2020-02-14 15:29:11');

----------------------输血输血3小时(结束)-------------------


----------------------输血输血4小时(开始)-------------------
IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6100,4748574105933710009,N'temperature',N'体温',N'体温',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"°C","ItemDataSet":""}',10,1,N'2020-02-14 14:54:56');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6200)
INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6200,4748574105933710009,N'bloodpressure',N'血压',N'血压',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}',20,1,N'2020-02-14 15:22:22');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6300,4748574105933710009,N'pulse',N'脉博',N'脉博',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"/min","ItemDataSet":""}',30,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6400,4748574105933710009,N'pulse',N'滴速',N'滴速',N'{"ItemXType":"numberfield","ItemDefaultValue":"","ItemUnit":"滴/分","ItemDataSet":""}',40,1,N'2020-02-14 15:24:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6500,4748574105933710009,N'other',N'患者一般情况',N'患者一般情况',N'{"ItemXType":"uxSimpleComboBox","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":"[{''正常'':''正常''},{''异常'':''异常''}]"}',50,1,N'2020-02-14 15:29:11');

----------------------输血输血4小时(结束)-------------------

----------------------输血临床处理措施(开始)-------------------

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4703071337826980944) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4703071337826980944,4780816011182130210,N'10',N'立即停止输血,保持静脉通路',N'10',10,1,N'2020-02-14 17:19:27');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5215863475154392192) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5215863475154392192,4780816011182130210,N'20',N'对症处理',N'30',30,1,N'2020-02-14 17:20:27');

----------------------输血临床处理措施(结束)-------------------