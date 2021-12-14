 Ext.define("Shell.class.opd.basic.AdvancedQuery", {
    extend: 'Shell.ux.panel.AppPanel',
    itemStyle: "margin-top:5px,margin-left:5px",
    width:600,
    height:550,
    layout: {
        type: 'table',
        columns:8,
        tableAttrs: {
            cellpadding: 1,
            cellspacing: 1,
            width: '100%',
            //style: 'margin-top:40px',
            align: 'right'
            
        }
    },
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.dockedItems = me.createDockedItems();       	
        me.callParent(arguments);
    },
    afterRender:function () {
        var me = this;
        me.callParent(arguments);
    },
	
    createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            items: [{
                xtype: 'button', text: '确定',
                iconCls: 'button-save',
                listeners: {
                    click: function () {
                    	var  where = me.getValue();
				    	if(where == ""){
				    		Shell.util.Msg.showInfo("请选择查询条件！");
				    		return;
				    	}
				    	me.fireEvent('save',where);
				    	
                    }
                }
            },{
                xtype: 'button', text: '取消',
                iconCls: 'button-cancel',
                listeners: {
                    click: function () {
                        me.close();
                    }
                }
            }
            ]
        });
        return [tooblar];
    },
    getValue:function(){
    	var me = this;
    	var items = me.items.items;
    	var radios = [];
    	var fieldset = [];
    	var sqlwhere ="           1=1 ";
    	
    	var selectdate = me.getComponent("selectdate").getValue();
    	var CName = me.getComponent("CName").getValue();
    	var PatNo = me.getComponent("PatNo").getValue();
    	var Serialno = me.getComponent("Serialno").getValue();
    	var clientNo = me.getComponent("clientNo").getValue();
    	var itemName = me.getComponent("itemName").getValue();
    	var SectionNo = me.getComponent("SectionNo").getValue();
    	
    	
    	for(var i=0;i<items.length;i++){
    		if(items[i].xtype == "radio" && items[i].checked){
    			radios.push(items[i]);
    		}else if(items[i].xtype == "fieldset"){
    			fieldset.push(items[i]);
    		}
    	}
    	    	
    	//时间条件拼接-----------------------------------------------------
    	if(selectdate && selectdate != null  && selectdate != ""){
    		for(var a=0;a<radios.length;a++){
    			if(radios[a].name == "queryDate"){
	    			var conditionValue = radios[a].conditionValue;
					var start = Shell.util.Date.toString(selectdate.start);
					var end = Shell.util.Date.toString(selectdate.end);
					if( start != null && end == null){
						sqlwhere += " and " + conditionValue + "='" + start + "'";
					}else if (start == null && end != null){
						sqlwhere += " and " + conditionValue + "='" + end + "'";
					}else{
						sqlwhere += " and " + conditionValue + ">='" + start + "' and "+ conditionValue + "<='" + end + "'";
					}
	    		}
    		}
    	}
    	
    	//姓名条件拼接---------------------------------------------------
    	if(CName && CName != "" && CName != null){
    		var selectSqlCName = "";
    		var selectCName = "";
    		for(var a=0;a<radios.length;a++){
    			if(radios[a].name == "selectSqlCName"){
    				selectSqlCName = radios[a]. conditionValue;
    			}else if(radios[a].name == "selectCName"){
    				selectCName = radios[a]. conditionValue;
    			}
    		}
    		if(selectCName != "" && selectSqlCName != ""){
    			var cnames = CName.split(' ');
    			sqlwhere += " and ( "
    			for(var i = 0 ; i<cnames.length;i++){
    				if(i == 0){
    					if(selectCName == "like"){
    						sqlwhere +=  " CName " + selectCName + " '%"+ cnames[i] + "%' ";
    					}else{
    						sqlwhere +=  " CName " + selectCName + " '"+ cnames[i] + "' ";
    					}    					
    				}else{
    					if(selectCName == "like"){
    						sqlwhere += " "+ selectSqlCName +" CName " + selectCName + " '%"+ cnames[i] + "%'";
    					}else{
    						sqlwhere += " "+ selectSqlCName +" CName " + selectCName + " '"+ cnames[i] + "'";
    					}   
    					
    				}
    			}
    			sqlwhere += " )";
    		}
    	}
    	
    	//病历号条件拼接---------------------------------------------------
    	if(PatNo && PatNo != "" && PatNo != null){
    		var selectSqlPatNo = "";
    		var selectPatNo = "";
    		for(var a=0;a<radios.length;a++){
    			if(radios[a].name == "selectSqlPatNo"){
    				selectSqlPatNo = radios[a]. conditionValue;
    			}else if(radios[a].name == "selectPatNo"){
    				selectPatNo = radios[a]. conditionValue;
    			}
    		}
    		if(selectPatNo != "" && selectSqlPatNo != ""){
    			var PatNos = PatNo.split(' ');
    			sqlwhere += " and ( "
    			for(var i = 0 ; i<PatNos.length;i++){
    				if(i == 0){
    					if(selectCName == "like"){
    						sqlwhere +=  " PatNo " + selectPatNo + " '%"+ PatNos[i] + "%' ";
    					}else{
    						sqlwhere +=  " PatNo " + selectPatNo + " '"+ PatNos[i] + "' ";
    					}   
    				}else{
    					if(selectCName == "like"){
    						sqlwhere += " "+ selectSqlPatNo +" PatNo " + selectPatNo + " '%"+ PatNos[i] + "%' ";
    					}else{
    						sqlwhere += " "+ selectSqlPatNo +" PatNo " + selectPatNo + " '"+ PatNos[i] + "' ";
    					} 
    				}
    			}
    			sqlwhere += " )";
    		}
    	}
		
		//条码号条件拼接---------------------------------------------------
    	if(Serialno && Serialno != "" && Serialno != null){
    		var selectSqlSerialno = "";
    		var selectSerialno = "";
    		for(var a=0;a<radios.length;a++){
    			if(radios[a].name == "selectSqlSerialno"){
    				selectSqlSerialno = radios[a]. conditionValue;
    			}else if(radios[a].name == "selectSerialno"){
    				selectSerialno = radios[a]. conditionValue;
    			}
    		}
    		if(selectSerialno != "" && selectSqlSerialno != ""){
    			var Serialnos = Serialno.split(' ');
    			sqlwhere += " and ( "
    			for(var i = 0 ; i<Serialnos.length;i++){
    				if(i == 0){
    					if(selectCName == "like"){
    						sqlwhere += " Serialno " + selectSerialno + " '%"+ Serialnos[i] + "%' ";
    					}else{
    						sqlwhere += " Serialno " + selectSerialno + " '"+ Serialnos[i] + "' ";
    					} 
    				}else{
    					if(selectCName == "like"){
    						sqlwhere += " " + selectSqlSerialno + " Serialno " + selectSerialno + " '%"+ Serialnos[i] + "%' ";
    					}else{
    						sqlwhere += " " + selectSqlSerialno + " Serialno " + selectSerialno + " '"+ Serialnos[i] + "' ";
    					}
    				}
    			}
    			sqlwhere += " )";
    		}
    	}
		
		//送检单位条件拼接-------------------------------------------------
		if(clientNo && clientNo != "" && clientNo != null){
    		var selectSqlclientNo = "";
    		var selectclientNo = "";
    		for(var a=0;a<radios.length;a++){
    			if(radios[a].name == "selectSqlclientNo"){
    				selectSqlclientNo = radios[a]. conditionValue;
    			}else if(radios[a].name == "selectclientNo"){
    				selectclientNo = radios[a]. conditionValue;
    			}
    		}
    		if(selectclientNo != "" && selectSqlclientNo != ""){
    			var clientNos = clientNo.split(' ');
    			sqlwhere += " and ( "
    			for(var i = 0 ; i<clientNos.length;i++){
    				if(i == 0){
    					if(selectCName == "like"){
    						sqlwhere += " clientNo " + selectclientNo + " '%"+ clientNos[i] + "%' ";
    					}else{
    						sqlwhere += " clientNo " + selectclientNo + " '"+ clientNos[i] + "' ";
    					}
    				}else{
    					if(selectCName == "like"){
    						sqlwhere += " " + selectSqlclientNo + " clientNo " + selectclientNo + " '%"+ clientNos[i] + "%' ";
    					}else{
    						sqlwhere += " " + selectSqlclientNo + " clientNo " + selectclientNo + " '"+ clientNos[i] + "' ";
    					}
    				}
    			}
    			sqlwhere += " )";
    		}
    	}
		
    	//检验项目条件拼接-------------------------------------------------
		if(itemName && itemName != "" && itemName != null){
    		var selectSqlitemNo = "";
    		var selectitemNo = "";
    		for(var a=0;a<radios.length;a++){
    			if(radios[a].name == "selectSqlitemNo"){
    				selectSqlitemNo = radios[a]. conditionValue;
    			}else if(radios[a].name == "selectitemNo"){
    				selectitemNo = radios[a]. conditionValue;
    			}
    		}
    		if(selectitemNo != "" && selectSqlitemNo != ""){
    			var itemNames = itemName.split(' ');
    			sqlwhere += " and ( "
    			for(var i = 0 ; i < itemNames.length;i++){
    				if(i == 0){
    					if(selectCName == "like"){
    						sqlwhere += " itemName " + selectitemNo + " '%"+ itemNames[i] + "%' ";
    					}else{
    						sqlwhere += " itemName " + selectitemNo + " '"+ itemNames[i] + "' ";
    					}
    				}else{
    					if(selectCName == "like"){
    						sqlwhere += " " + selectSqlitemNo + " itemName " + selectitemNo + " '%"+ itemNames[i] + "%' ";
    					}else{
    						sqlwhere += " " + selectSqlitemNo + " itemName " + selectitemNo + " '"+ itemNames[i] + "' ";
    					}
    				}
    			}
    			sqlwhere += " )";
    		}
    	}	
    	
    	//检验小组条件拼接-------------------------------------------------
		if(SectionNo && SectionNo != "" && SectionNo != null){
    		var selectSqlpGroup = "";
    		var selectpGroup = "";
    		for(var a=0;a<radios.length;a++){
    			if(radios[a].name == "selectSqlpGroup"){
    				selectSqlpGroup = radios[a]. conditionValue;
    			}else if(radios[a].name == "selectpGroup"){
    				selectpGroup = radios[a]. conditionValue;
    			}
    		}
    		if(selectpGroup != "" && selectSqlpGroup != ""){
    			var SectionNos = SectionNo.split(' ');
    			sqlwhere += " and ( "
    			for(var i = 0 ; i < SectionNos.length;i++){
    				if(i == 0){
    					if(selectCName == "like"){
    						sqlwhere += " SectionNo " + selectpGroup + " '%"+ SectionNos[i] + "%' ";
    					}else{
    						sqlwhere += " SectionNo " + selectpGroup + " '"+ SectionNos[i] + "' ";
    					}    					
    				}else{
    					if(selectCName == "like"){
    						sqlwhere += " " + selectSqlpGroup + " SectionNo " + selectpGroup + " '%"+ SectionNos[i] + "%' ";
    					}else{
    						sqlwhere += " " + selectSqlpGroup + " SectionNo " + selectpGroup + " '"+ SectionNos[i] + "' ";
    					}
    				}
    			}
    			sqlwhere += " )";
    		}
    	}	
    	
    	//拼接结果状态和报告状态----------------------------------------
    	for(var i = 0; i < fieldset.length; i++){
    		var fieldsetItems = fieldset[i].items.items;
    		//结果状态
    		if(fieldset[i].name == "fieldsetResultStatus"){//判断时候都是结果状态
    			var fieldsetItemsItems = [];
    			for(var b = 0;b<fieldsetItems.length;b++){//读取fieldset下的item
    				if(fieldsetItems[b].checked){
    					fieldsetItemsItems.push(fieldsetItems[b]);
    				}
    			} 
    			if(fieldsetItemsItems.length > 0){//判断是否选中选项
    				var selectSqlResultStatus = "";
	    			for(var a=0;a<radios.length;a++){//获取选中的选项
	    				if(radios[a].name = "selectSqlResultStatus"){
	    					selectSqlResultStatus = radios[a]. conditionValue;
	    				}
	    			}
	    			sqlwhere += " and ( "
	    			for(var c=0;c<fieldsetItemsItems.length;c++){//拼接查询条件
	    				if(c == 0){
	    					sqlwhere += " ResultStatus = '"+fieldsetItemsItems[c].conditionValue+"' ";
	    				}else{
	    					sqlwhere += " or ResultStatus = '"+fieldsetItemsItems[c].conditionValue+"' ";
	    				}
	    				
	    			}
	    			sqlwhere += " )";
    			}
    		}else if(fieldset[i].name=="fieldsetReportStatus"){//判断是否是报告状态
    			
					if(fieldsetItems[0].checked && fieldsetItems[1].checked){
						sqlwhere += " and ISNULL(PrintTimes,0)>=0 ";
					}else{
						for(var a=0;a<fieldsetItems.length;a++){
						 	if(fieldsetItems[a].checked){
						 		if(fieldsetItems[a].conditionValue=="0"){
						 			sqlwhere += " and ISNULL(PrintTimes,0) < 1";
						 		}else if(fieldsetItems[a].conditionValue=="1"){
						 			sqlwhere += " and ISNULL(PrintTimes,0) > 0";
						 		}
							 	
						 	}
						}	
					}
    		}
    	}
   		return sqlwhere.slice(4);
    },
    createItems: function () {
        var me = this;
        var items = [];
        
        
        //时间-----------------------------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'时间格式',
            colspan:2
        },{
             xtype: 'radio',
             style: me.itemStyle,
             name: 'queryDate',
             itemId: 'CHECKDATE',
             boxLabel: '审核时间',
             conditionValue:'CHECKDATE',
             checked:true,
             colspan:2
         }, {
             xtype: 'radio',
             name: 'queryDate',
             itemId: 'COLLECTDATE',
             style: me.itemStyle,
             boxLabel: '采样时间',
             conditionValue:'COLLECTDATE',
             colspan:2
         }, {
             xtype: 'radio',
             name: 'queryDate',
             itemId: 'RECEIVEDATE',
             style: me.itemStyle,
             boxLabel: '核收时间',
             conditionValue:'RECEIVEDATE',
             colspan:2
         },{
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'时间范围',
            colspan:2
         }, { 
        	xtype: 'uxdatearea', 
        	itemId: 'selectdate', 
        	name: 'selectdate',
        	style: me.itemStyle,
        	value:{start: Shell.util.Date.getNextDate(new Date(), 1 - 3),end:new Date()},
        	colspan:6
        });        
        //姓名-----------------------------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'姓名',
        	colspan:1
        },{
             xtype: 'radio',
             name:'selectSqlCName',
             style: me.itemStyle,
             boxLabel: '与',
             conditionValue:'or',
        	 colspan:1,
             checked:true
             
         }, {
             xtype: 'label',
             style: me.itemStyle,
             text:'  (',
        	 colspan:1
         },{
        	xtype: 'textfield',
            style: me.itemStyle,
            fieldLabel: '',
            itemId:'CNameWaiting',
        	colspan:1,
            enableKeyEvents: true,
            listeners: {
                render: function (field) {
                    //回车按键监听
                    new Ext.KeyMap(field.getEl(), [{
                        key: Ext.EventObject.ENTER,
                        fn: function () {
                           var CNameWaiting = me.getComponent("CNameWaiting").value;
                           var cname = me.getComponent("CName").value;
                           if(cname){
                           	 me.getComponent("CName").setValue(cname+" "+CNameWaiting);
                           }else{
                           	 me.getComponent("CName").setValue(CNameWaiting);
                           }
                        }
                    }]);
                }
            }
        }, {
             xtype: 'radio',
             name:'selectCName',
             style: me.itemStyle,
             boxLabel: '等于',
             conditionValue:'=',
        	 colspan:1,
             checked:true
         }, {
             xtype: 'radio',
             name:'selectCName',
             style: me.itemStyle,
             boxLabel: '相似',
             conditionValue:'like',
        	 colspan:1
         }, {
             xtype: 'radio',
             name:'selectCName',
             style: me.itemStyle,
             boxLabel: '不等',
             conditionValue:'!=',
        	 colspan:1
         },{
            xtype:'label',
           // width:120,
            style: me.itemStyle,
            text:')',
        	colspan:1
         }, { 
        	xtype: 'textfield', 
        	name: 'CName',
        	itemId:'CName',
        	style: me.itemStyle,
        	width:550,
        	disabled:true,
            fieldLabel: '',
        	colspan:7
        },{
        	xtype:'button',
        	text:'...',
        	style: me.itemStyle,
        	colspan:1,
        	listeners:{
        		 click: function() {
		            me.getComponent("CName").setValue("");
		        }
        	}
        });
        
        //病历号-----------------------------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'病历号',
        	colspan:1
        },{
             xtype: 'radio',
             name:'selectSqlPatNo',
             style: me.itemStyle,
             boxLabel: '与',
             conditionValue:'or',
        	 colspan:1,
             checked:true
         }, {
             xtype: 'label',
             style: me.itemStyle,
             text:'  (',
        	 colspan:1
         },{
        	xtype: 'textfield',
            style: me.itemStyle,
            fieldLabel: '',
            itemId:'PatNoWaiting',
        	colspan:1,
        	enableKeyEvents: true,
            listeners: {
                render: function (field) {
                    //回车按键监听
                    new Ext.KeyMap(field.getEl(), [{
                        key: Ext.EventObject.ENTER,
                        fn: function () {
                           var PatNoWaiting = me.getComponent("PatNoWaiting").value;
                           var PatNo = me.getComponent("PatNo").value;
                           if(PatNo){
                           	 me.getComponent("PatNo").setValue(PatNo+" "+PatNoWaiting);
                           }else{
                           	 me.getComponent("PatNo").setValue(PatNoWaiting);
                           }
                        }
                    }]);
                }
            }
        }, {
              xtype: 'radio',
             name:'selectPatNo',
             style: me.itemStyle,
             boxLabel: '等于',
             conditionValue:'=',
        	 colspan:1,
             checked:true
         }, {
              xtype: 'radio',
             name:'selectPatNo',
             style: me.itemStyle,
             boxLabel: '相似',
             conditionValue:'like',
        	 colspan:1
         }, {
             xtype: 'radio',
             name:'selectPatNo',
             style: me.itemStyle,
             boxLabel: '不等',
             conditionValue:'!=',
        	colspan:1
         },{
            xtype:'label',
           // width:120,
            style: me.itemStyle,
            text:')',
        	colspan:1
         }, { 
        	xtype: 'textfield', 
        	itemId:'PatNo',
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	colspan:7,
        	disabled:true
        },{
        	xtype:'button',
        	text:'...',
        	style: me.itemStyle,
        	colspan:1,
        	listeners:{
        		 click: function() {
		            me.getComponent("PatNo").setValue("");
		        }
        	}
        });
        
        //条码号-----------------------------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'条码号',
        	colspan:1
        },{
             xtype: 'radio',
             name:'selectSqlSerialno',
             style: me.itemStyle,
             boxLabel: '与',
             conditionValue:'or',
        	 colspan:1,
             checked:true
             
         }, {
             xtype: 'label',
             style: me.itemStyle,
             text:'  (',
        	 colspan:1
         },{
        	xtype: 'textfield',
            style: me.itemStyle,
            fieldLabel: '',
        	colspan:1,
        	itemId:'SerialnoWaiting',
        	listeners: {
                render: function (field) {
                    //回车按键监听
                    new Ext.KeyMap(field.getEl(), [{
                        key: Ext.EventObject.ENTER,
                        fn: function () {
                           var SerialnoWaiting = me.getComponent("SerialnoWaiting").value;
                           var Serialno = me.getComponent("Serialno").value;
                           if(Serialno){
                           	 me.getComponent("Serialno").setValue(Serialno+" "+SerialnoWaiting);
                           }else{
                           	 me.getComponent("Serialno").setValue(SerialnoWaiting);
                           }
                        }
                    }]);
                }
            }
        }, {
             xtype: 'radio',
             name:'selectSerialno',
             style: me.itemStyle,
             boxLabel: '等于',
             conditionValue:'=',
        	 colspan:1,
             checked:true
         }, {
             xtype: 'radio',
             name:'selectSerialno',
             style: me.itemStyle,
             boxLabel: '相似',
             conditionValue:'like',
        	 colspan:1
         }, {
             xtype: 'radio',
             name:'selectSerialno',
             style: me.itemStyle,
             boxLabel: '不等',
             conditionValue:'!=',
        	 colspan:1
         },{
            xtype:'label',
           // width:120,
            style: me.itemStyle,
            text:')',
        	colspan:1
         }, { 
        	xtype: 'textfield', 
        	itemId: 'Serialno',
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	colspan:7,
        	disabled:true
        },{
        	xtype:'button',
        	text:'...',
        	style: me.itemStyle,
        	colspan:1,
        	listeners:{
        		 click: function() {
		            me.getComponent("Serialno").setValue("");
		        }
        	}
        });
        
        //送检单位-----------------------------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'送检单位',
        	colspan:1
        },{
             xtype: 'radio',
             name:'selectSqlclientNo',
             style: me.itemStyle,
             boxLabel: '与',
             conditionValue:'or',
        	 colspan:1,
             checked:true
             
         }, {
             xtype: 'label',
             style: me.itemStyle,
             text:'  (',
        	 colspan:1
         },{
            xtype: 'uxCheckTrigger',
            width: 150,
            style: me.itemStyle,
            labelSeparator: '',
            labelAlign: 'left',
            itemId: 'clientNoWaiting',
            colspan:1,
            className: 'Shell.class.dictionaries.clientele.clientele',
            listeners: {
                check: function (p, record) {
                    if (record == null) {
                    	 me.getComponent("clientNoWaiting").setValue("");
                    	return;
	                }
                    
                	me.getComponent("clientNoWaiting").setValue(record.get("CNAME"));
                	
                	var clientName = me.getComponent("clientName").value;
                	var clientNo = me.getComponent("clientNo").value;
                	
                	if(!clientName || clientName=="" || clientName == null){
                		me.getComponent("clientName").setValue(record.get("CNAME"));
                	}else{
                		me.getComponent("clientName").setValue(clientName + " " + record.get("CNAME"));
                	}
                	
                	if(!clientNo || clientNo=="" || clientNo == null){
                		me.getComponent("clientNo").setValue(record.get("ClIENTNO"));
                	}else{
                		me.getComponent("clientNo").setValue(clientNo + " " + record.get("ClIENTNO"));
                	}
                }
            }
        }, {
	         xtype: 'radio',
	         name:'selectclientNo',
	         style: me.itemStyle,
	         boxLabel: '等于',
	         conditionValue:'=',
	    	 colspan:1,
	         checked:true
         }, {
             xtype: 'radio',
             name:'selectclientNo',
             style: me.itemStyle,
             boxLabel: '相似',
             conditionValue:'like',
        	 colspan:1
         }, {
             xtype: 'radio',
             name:'selectclientNo',
             style: me.itemStyle,
             boxLabel: '不等',
             conditionValue:'!=',
        	 colspan:1
         },{
            xtype:'label',
           // width:120,
            style: me.itemStyle,
            text:')',
        	colspan:1
         }, { 
        	xtype: 'textfield', 
        	itemId:'clientName',
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	colspan:7,
        	disabled:true
        },{
        	xtype:'button',
        	text:'...',
        	style: me.itemStyle,
        	colspan:1,
        	listeners:{
        		 click: function() {
		            me.getComponent("clientName").setValue("");
		            me.getComponent("clientNo").setValue("");
		        }
        	}
        },{ 
        	xtype: 'textfield', 
			itemId:'clientNo',        	
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	disabled:true,
        	colspan:8,
        	hidden:true
        });
        
        //检验项目-----------------------------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'检验项目',
        	colspan:1
        },{
             xtype: 'radio',
             name:'selectSqlitemNo',
             style: me.itemStyle,
             boxLabel: '与',
             conditionValue:'or',
        	 colspan:1,
             checked:true
             
         }, {
             xtype: 'label',
             style: me.itemStyle,
             text:'  (',
        	 colspan:1
         },{
                xtype: 'uxCheckTrigger',
                width: 150,
                style: me.itemStyle,
                labelSeparator: '',
                labelAlign: 'left',
                itemId: 'itemNoWaiting',
                colspan:1,
                className: 'Shell.class.dictionaries.testitem.testItem',
                listeners: {
                    check: function (p, record) {
	                    if (record == null) {
	                    	 me.getComponent("itemNoWaiting").setValue("");
	                    	return;
		                }
	                    
                    	me.getComponent("itemNoWaiting").setValue(record.get("CName"));
                    	
                    	var itemName = me.getComponent("itemName").value;
                    	var ItemNo = me.getComponent("ItemNo").value;
                    	
                    	if(!itemName || itemName=="" || itemName == null){
                    		me.getComponent("itemName").setValue(record.get("CName"));
                    	}else{
                    		me.getComponent("itemName").setValue(itemName + " " + record.get("CName"));
                    	}
                    	
                    	if(!ItemNo || ItemNo=="" || ItemNo == null){
                    		me.getComponent("ItemNo").setValue(record.get("ItemNo"));
                    	}else{
                    		me.getComponent("ItemNo").setValue(ItemNo + " " + record.get("ItemNo"));
                    	}
                    }
                }
            }, {
             xtype: 'radio',
             name:'selectitemNo',
             style: me.itemStyle,
             boxLabel: '等于',
             conditionValue:'=',
        	 colspan:1,
             checked:true
         }, {
             xtype: 'radio',
             name:'selectitemNo',
             style: me.itemStyle,
             boxLabel: '相似',
             conditionValue:'like',
        	 colspan:1
         }, {
             xtype: 'radio',
             name:'selectitemNo',
             style: me.itemStyle,
             boxLabel: '不等',
             conditionValue:'!=',
        	 colspan:1
         },{
            xtype:'label',
           // width:120,
            style: me.itemStyle,
            text:')',
        	colspan:1
         }, { 
        	xtype: 'textfield', 
        	itemId:'itemName',
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	colspan:7,
        	disabled:true
        },{
        	xtype:'button',
        	text:'...',
        	style: me.itemStyle,
        	colspan:1,
        	listeners:{
        		 click: function() {
		            me.getComponent("itemName").setValue("");
		        }
        	}
        },{ 
        	xtype: 'textfield', 
			itemId:'ItemNo',        	
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	disabled:true,
        	colspan:8,
        	hidden:true
        });
        
        //检验小组-----------------------------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'检验小组',
        	colspan:1
        },{
             xtype: 'radio',
             name:'selectSqlpGroup',
             style: me.itemStyle,
             boxLabel: '与',
             conditionValue:'or',
        	 colspan:1,
             checked:true
             
         }, {
             xtype: 'label',
             style: me.itemStyle,
             text:'  (',
        	 colspan:1
         },{
                xtype: 'uxCheckTrigger',
                width: 150,
                style: me.itemStyle,
                labelSeparator: '',
                labelAlign: 'left',
                itemId: 'pGroupWaiting',
                colspan:1,
                className: 'Shell.class.dictionaries.pgroup.section2',
                listeners: {
                    check: function (p, record) {
	                    if (record == null) {
	                    	 me.getComponent("pGroupWaiting").setValue("");
	                    	return;
		                }
	                    
                    	me.getComponent("pGroupWaiting").setValue(record.get("CName"));
                    	
                    	var pGroupName = me.getComponent("pGroupName").value;
                    	var SectionNo = me.getComponent("SectionNo").value;
                    	
                    	if(!pGroupName || pGroupName=="" || pGroupName == null){
                    		me.getComponent("pGroupName").setValue(record.get("CName"));
                    	}else{
                    		me.getComponent("pGroupName").setValue(pGroupName + " " + record.get("CName"));
                    	}
                    	
                    	if(!SectionNo || SectionNo=="" || SectionNo == null){
                    		me.getComponent("SectionNo").setValue(record.get("SectionNo"));
                    	}else{
                    		me.getComponent("SectionNo").setValue(SectionNo + " " + record.get("SectionNo"));
                    	}
                    }
                }
            }, {
             xtype: 'radio',
             name:'selectpGroup',
             style: me.itemStyle,
             boxLabel: '等于',
             conditionValue:'=',
        	 colspan:1,
             checked:true
         }, {
             xtype: 'radio',
             name:'selectpGroup',
             style: me.itemStyle,
             boxLabel: '相似',
             conditionValue:'like',
        	 colspan:1
         }, {
             xtype: 'radio',
             name:'selectpGroup',
             style: me.itemStyle,
             boxLabel: '不等',
             conditionValue:'!=',
        	 colspan:1
         },{
            xtype:'label',
           // width:120,
            style: me.itemStyle,
            text:')',
        	colspan:1
         }, { 
        	xtype: 'textfield', 
			itemId:'pGroupName',        	
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	colspan:7,
        	disabled:true
        },{
        	xtype:'button',
        	text:'...',
        	style: me.itemStyle,
        	colspan:1,
        	listeners:{
        		 click: function() {
		            me.getComponent("pGroupName").setValue("");
		            me.getComponent("SectionNo").setValue("");
		        }
        	}
        },{ 
        	xtype: 'textfield', 
			itemId:'SectionNo',        	
        	style: me.itemStyle,
        	width:550,
            fieldLabel: '',
        	disabled:true,
        	colspan:8,
        	hidden:true
        });
        
      
        
        //结果状态--------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'结果状态',
            colspan:1
        },{
             xtype: 'label',
             style: me.itemStyle,
             text:'',
        	 colspan:1
         },{
             xtype: 'label',
             style: me.itemStyle,
             text:'',
        	 colspan:1
         },{
            xtype: 'fieldset',
            style: me.itemStyle,
            name:'fieldsetResultStatus',
            defaultType: 'checkbox',
            width:450,
            colspan:5,
            layout: 'column',
            items: [{
		             xtype: 'checkbox',
		             name:'selectResultStatusValue',
		             style: me.itemStyle,
		             columnWidth:0.2,
                     conditionValue:'H',
		             boxLabel: '高值H'
		         }, {
		             xtype: 'checkbox',
		             name:'selectResultStatusValue',
		             style: me.itemStyle,
		             columnWidth:0.2,
                     conditionValue:'L',
		             boxLabel: '低值L'
		    }, {
		             xtype: 'checkbox',
		             name:'selectResultStatusValue',
		             style: me.itemStyle,
		             columnWidth:0.2,
                     conditionValue:'HH',
		             boxLabel: '异常高HH'
		    }, {
		             xtype: 'checkbox',
		             name:'selectResultStatusValue',
		             style: me.itemStyle,
		             columnWidth:0.2,
                     conditionValue:'LL',
		             boxLabel: '异常低LL'
		    }, {
		             xtype: 'checkbox',
		             name:'selectResultStatusValue',
		             style: me.itemStyle,
		             columnWidth:0.2,
                     conditionValue:'',
		             boxLabel: '其他'
		    }]
        });
        
        //报告状态----------------------------
        items.push({
            xtype:'label',
            width:120,
            style: me.itemStyle,
            text:'报告状态',
            colspan:1
        },{
            xtype: 'fieldset',
            style: me.itemStyle,
            name:'fieldsetReportStatus',
            defaultType: 'checkbox',
            width:400,
            colspan:8,
            layout: 'column',
            items: [{
		             xtype: 'checkbox',
		             name:'reportStatus',
		             style: me.itemStyle,
		             columnWidth:0.5,
                     conditionValue:'1',
		             boxLabel: '已打印'
		         }, {
		             xtype: 'checkbox',
		             name:'reportStatus',
		             style: me.itemStyle,
		             columnWidth:0.5,
                     conditionValue:'0',
		             boxLabel: '未打印'
		    }]
        });
        
        return items;
    }
});