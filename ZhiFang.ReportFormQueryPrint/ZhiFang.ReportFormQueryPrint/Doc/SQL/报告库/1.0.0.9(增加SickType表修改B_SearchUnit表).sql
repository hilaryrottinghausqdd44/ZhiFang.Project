--郭海祥2019-1-4 解决查询项查询字典问题
-- 修改B_SearchUnit中就诊类型查询项数据JsCode
update [dbo].[B_SearchUnit] set JsCode = '{ type: ''search'', xtype: ''textfield'', mark: ''='', itemId: ''SICKTYPENAME'', name: ''SICKTYPENAME'', width: 130, hidden: true } searchAndNext
            {
                fieldLabel: '''',
                xtype: ''uxCheckTrigger'',
                emptyText: ''就诊类型'',
                zIndex:1,
                width: 150,
                labelSeparator: '''',
                labelWidth: 55,
                labelAlign: ''right'',
                itemId: ''SickTypeNo'',
                className: ''Shell.class.sicktype.SickType'',
                listeners: {
                    check: function (p, record) {
                        var item = "";
                        var clientName = "";
                        var me = this.ownerCt.ownerCt;
	                    if (record == null) {
                            item = me.getItem("SICKTYPENAME");
                            clientName = me.getItem("SickTypeNo");
                            item.setValue("");
                            clientName.setValue("");
                            return;
                        }
						
                        item = me.getItem("SICKTYPENAME");
                        clientName = me.getItem("SickTypeNo");
                        item.setValue(  record.get("CName") );
                        clientName.setValue(record.get("CName"));
                        p.close();
                    }
                }' where SID = 2
GO 


