 IF COL_LENGTH('SC_Operation', 'TypeName') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN TypeName varchar(50);  
                
IF COL_LENGTH('SC_Operation', 'BusinessModuleCode') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN BusinessModuleCode varchar(50);  
                
 IF COL_LENGTH('SC_Operation', 'Memo') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN Memo text;  

 IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}' WHERE [TransRecordTypeItemID] = 1200; 
                
IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}' WHERE [TransRecordTypeItemID] = 2200; 
                
 IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}' WHERE [TransRecordTypeItemID] = 3200; 
                
IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}' WHERE [TransRecordTypeItemID] = 4200; 
                
IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}' WHERE [TransRecordTypeItemID] = 5200; 
                
 IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"mmhg","ItemDataSet":""}' WHERE [TransRecordTypeItemID] = 6200; 
                