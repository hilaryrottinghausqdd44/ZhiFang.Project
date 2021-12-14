/**
 * 数据登记表单组件
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.operate.Form', {
	extend: 'Shell.ux.form.Panel',
	bodyPadding: '5px 5px 5px 20px',
	formtype: "add",
	layout: {
		type: 'table',
		columns: 2,
		tdAttrs: {　
			style: "border-bottom:1px #EDEDED solid;border-left:none;border-right:none;border-top:none;"
		}
	},
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.class.qms.equip.templet.operate.DataTimeForm',
		'Shell.class.qms.equip.templet.operate.MultiComboBox',
		'Shell.class.qms.equip.templet.operate.ComBoxGroup',
		'Shell.class.qms.equip.templet.operate.CheckBoxGroup',
		'Shell.class.qms.equip.templet.operate.RadioGroup',
		'Shell.class.qms.equip.templet.operate.Number'
	],
	border:false,
	showSuccessInfo: false,
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 5,width: 175,
		labelAlign: 'right',labelSeparator: ''
	},
	/**定制服务,非TB 获取数据服务路径*/
	selectUrl2: '/QMSReport.svc/QMS_UDTO_SearchEMaintenanceData?isPlanish=true',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEMaintenanceDataByHQL?isPlanish=true',
	/**获取参数数据服务路径*/
	selectParaUrl: '/QMSReport.svc/ST_UDTO_SearchEParameterByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/ST_UDTO_AddEMaintenanceData',
	/**编辑服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEMaintenanceDataByField',
	/**删除服务地址*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelEMaintenanceData',
	/**模板*/
	TempletID: '',
	height: 240,
	width: 440,
	title: "仪器维护数据表",
	formtype: 'show',
	/**数据主键*/
	PK: '',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:true,
    TYPE:'',
    
    /**默认时间显示*/
    defalutDate:null,
    /**功能按钮栏位置*/
	buttonDock: 'top',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var dailybtn = me.getComponent('buttonsToolbar').getComponent('dailybtn2');
		var ParaValue='0'; 
		me.getEParaVal(function(data){
			if(data){
				var len =data.list.length;
				if(len>0){
					ParaValue = data.list[0].EParameter_ParaValue;
				}
			}
		});
		if(ParaValue=='1'){
			dailybtn.setVisible(true);
		}else{
			dailybtn.setVisible(false);
		}
	},
	initComponent: function() {
		var me = this;
		 /**默认时间显示*/
		me.iniDate();
		 /**时间改变事件对外公开*/
		me.addEvents('blur','onDailyClick');
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/*时间初始化*/
	iniDate:function(){
		var me=this;
	    var Sysdate = JcallShell.System.Date.getDate();
		var operatedate = JcallShell.Date.toString(Sysdate, true);
       me.defalutDate=operatedate;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me=this;
		var isreadOnly=false;
		var iseditable =true;
		var buttonToolbarItems =[];
		if(me.ISEDITDATE=='false'){
			buttonToolbarItems=[{
				width:100,xtype: 'datefield',
				format: 'Y-m-d',emptyText:'必填项',allowBlank:false,
				value:me.defalutDate,itemId: 'EMaintenanceData_Date',name: 'EMaintenanceData_Date',
				listeners :{
					blur :function ( com, The, eOpts ){
						me.fireEvent('blur', com);
					}
				}
			}];
		}else{
			buttonToolbarItems=[{
				xtype: 'label',text: me.defalutDate,margin: '0 0 1 10',
				style: "font-weight:bold;color:blue;",itemId: 'EMaintenanceData_Date',name: 'EMaintenanceData_Date'
			}];
		}
		buttonToolbarItems.push('-',{
			type: 'button',
				iconCls: 'button-import hand',
				itemId:'dailybtn2',
				text: '载入上次数据',
				handler: function(com) {
					me.fireEvent('onDailyClick', com);
				}
			},'->',{text:'填表说明',tooltip:'填表说明',iconCls:'button-help',
			handler: function() {
				me.showInfoById(me.TempletID);
			}
		});
		return buttonToolbarItems;
	},
	createItems: function(items) {
		var me = this;
		return items;
	},
	/*创建Id*/
	createNo: function(fieldLabel, i) {
		var me = this;
		var obj = {
			fieldLabel: fieldLabel,hidden: true,
			itemId: 'EMaintenanceData_Id' + i,name: 'EMaintenanceData_Id' + i
		};
		return obj;
	},
	/*创建项目组件*/
	createTempletItem: function(text, i,length,maxWidth) {
		var me = this;
		var str=""+ text +"";
		var obj = {
            xtype:"displayfield", type:"displayfield",
            style: "white-space:normal; word-break:break-all; ",
            fieldLabel:str, labelAlign:'left', width:length+20,
		    labelWidth:length,maxWidth:maxWidth,minWidth:55,
			name: 'EMaintenanceData_TempletItem' + i,
			itemId: 'EMaintenanceData_TempletItem' + i
		};
		return obj;
	},
	/*类型编码TempletItemCode*/
	createTempletItemCode: function(text,i) {
		var me = this;
		var str=""+ text +"";
		var obj = {
            xtype:"displayfield", type:"displayfield",hidden:true,
            style: "white-space:normal; word-break:break-all; ",
            fieldLabel:str, labelAlign:'left', width:100,
			name: 'EMaintenanceData_TempletItemCode' + i,
			itemId: 'EMaintenanceData_TempletItemCode' + i
		};
		return obj;
	},
	/*创建下拉框组件*/
	createComboBox: function(fieldLabel, i, DefaultValue, maxValue, minValue,StatusList) {
		var me = this;
		var obj = {
			xtype: 'uxSimpleComboBox',hasStyle: true,
			data: StatusList,value: DefaultValue,
			labelAlign:'center',
			maxValue: maxValue,minValue: minValue,
			width: me.defaults.width * 1,fieldLabel: fieldLabel,
			tooltip: fieldLabel,itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i
		};
		return obj;
	},
	/*创建多选下拉框组件(带复选框)*/
	createMultiComboBox: function(fieldLabel, i, DefaultValue, maxValue, minValue,StatusList) {
		var me = this;
		var obj = {
			xtype: 'multicombobox',hasStyle: true,
			data: StatusList,value: DefaultValue,
			maxValue: maxValue,minValue: minValue,
			width: me.defaults.width * 1,fieldLabel: fieldLabel,
			tooltip: fieldLabel,itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i
		};
		return obj;
	},
	/**创建复选框组件*/
	createCheckbox: function(fieldLabel, i,DefaultValue) {
		var me = this,val=false;
		//默认值设置1,默认选中 0 不选
		var strVal=DefaultValue+'';
	    val=false;
		if(strVal && strVal=='1'){
			val=true;	
		}
		var obj = {
			xtype: 'checkbox',boxLabel: '',
			inputValue: 'true',uncheckedValue: 'false',
			width: me.defaults.width * 1,checked: val,
			type: 'checkboxfield',fieldLabel: fieldLabel,
			tooltip: fieldLabel,itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i
		};
		return obj;
	},

	/*创建日期型组件*/
	createDate: function(fieldLabel, i, DefaultValue,InitItemCode) {
		var me = this;
		var defalutTime='';
		var Sysdate =JShell.System.Date.getDate();
	    //当前时间
        var TDate = JShell.Date.toString(Sysdate, true);
		if(DefaultValue.toUpperCase()=='CT'){
			var Sysdate = JcallShell.System.Date.getDate();
		    defalutTime = JcallShell.Date.toString(Sysdate,true);
		}
		var obj = {
			fieldLabel: fieldLabel,colspan: 1,
			value: defalutTime,
			width: me.defaults.width * 1,itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i,xtype: 'datefield',format: 'Y-m-d'
		};
		if(InitItemCode=='1'){
            obj.listeners={
            	change:function(com,newValue,oldValue,eOpts ){
            		var CurDate = JcallShell.Date.toString(newValue,true);
			        if(CurDate && InitItemCode=='1'){
			        	if(CurDate <= TDate ){
			        		com.setFieldStyle('color:red;');
				        }else{
				        	com.setFieldStyle('color:black;');
				        }
			        }
            	}
            };
		}
		return obj;
	},
	/*创建时间组件*/
	createTime: function(fieldLabel, i, DefaultValue) {
		var me = this;
		var defaultDate='';
    	if(DefaultValue.toUpperCase()=='CT'){
    		var Sysdate = JcallShell.System.Date.getDate();
		    var str = JcallShell.Date.toString(Sysdate);
		    if(str){
		    	defaultDate = str.substring(11, 20);
		    }
    	}
		var obj = {
			fieldLabel: fieldLabel,width: me.defaults.width * 1,
			value: defaultDate,itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i,xtype: 'timefield',format: 'H:i:s'
		};
		return obj;
	},

	/*创建日期时间组件*/
	createDataTime: function(fieldLabel, i,DefaultValue) {
		var me = this;
		var defaultTimeValue='',defaultDateValue='';
		if(DefaultValue.toUpperCase()=='CT'){
			var Sysdate = JcallShell.System.Date.getDate();
		    var str = JcallShell.Date.toString(Sysdate);
		    if(str){
		    	defaultTimeValue = str.substring(11, 20);
			    defaultDateValue = JcallShell.Date.toString(Sysdate,true);
		    }
		}
		var obj = {
			fieldLabel: fieldLabel, defaultDateValue:defaultDateValue,
		    defaultTimeValue:defaultTimeValue,width: me.defaults.width * 1,
			itemId: 'EMaintenanceData_ItemResult' + i,name: 'EMaintenanceData_ItemResult' + i,
			xtype: 'datatimefield'
		};
		return obj;
	},
	/**创建文本组件
	 * 最大宽度不能大于400
	 */
	createText: function(fieldLabel, i, DefaultValue, DataLength, maxValue, minValue,ItemHeight,ItemWidth,type) {
		var me = this;
		
		var obj = {
			fieldLabel: fieldLabel,xtype: 'textarea',
			value: DefaultValue,maxLength: DataLength,
			maxValue: maxValue,minValue: minValue,
			itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i
		};
		//tb类型 不能设置文本框大小
		if(!ItemWidth)ItemWidth=me.defaults.width * 1;
		if(!ItemHeight)ItemHeight=22;
		//当前是列表类型时,使用TB区分
	    if(type && type.length == 2 && 
	    	type.substr(0, 1).toUpperCase() == "T"){
	    	type='TB';
	    }
		if(type!='TB'){
			if(ItemWidth>400){
				ItemWidth=400;
			}
		}else{
			if(ItemWidth>300){
				ItemWidth=250;
			}
		}
		if(ItemWidth)obj.width=parseInt(ItemWidth);
		if(ItemHeight)obj.height=parseInt(ItemHeight) ;	
		
		return obj;
	},
	/*创建文本-下拉组件*/
	createComText: function(fieldLabel, i, data, DefaultValue, DataLength,editable) {
		var me = this;
		if(!editable)editable=false;
		var obj = {
			xtype: 'uxSimpleComboBox',hasStyle: true,
			value: DefaultValue,data: data,
			editable:editable,
			maxLength: DataLength,
			width: me.defaults.width * 1,
			fieldLabel: fieldLabel,tooltip: fieldLabel,
			itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i
		};
		return obj;
	},
	/*创建整型组件*/
	createNumber: function(fieldLabel, i, DefaultValue, maxValue, minValue, DecimalLength, allowDecimals,TempletID,AddValue,ItemCode,OperateDate) {
		var me = this;
		var obj = {
			AddValue:AddValue,
			ItemCode:ItemCode,
			TempletID:TempletID,
			OperateDate:OperateDate,
			width: me.defaults.width * 1,
			xtype: 'uxnumberfield',
			maxValue: maxValue,minValue: minValue,
			DecimalLength: DecimalLength, //这里允许保留3位小数
			allowDecimals: allowDecimals,DefaultValue: DefaultValue,
			step: 1,itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i
		};
		
		return obj;
	},
	/*创建模板备注*/
	createTempletMemo: function(obj) {
		var me = this;
		var width=280,val='';
	    var TempletMemoTitle = obj.TempletMemoTitle;
	    if(TempletMemoTitle){
	    	if(obj.TempletMemo){
		    	var TempletMemo ='&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'+ obj.TempletMemo;
				TempletMemo = TempletMemo.replace(/#/g, '<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;');
		        val=TempletMemoTitle+'<br />'+TempletMemo;
		    }
	    }else{
	    	var TempletMemo = obj.TempletMemo;
	    	TempletMemo = TempletMemo.replace(/#/g, '<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;');
		    val=TempletMemo;
	    }
		var obj = {
			fieldLabel: ' ',xtype: 'displayfield', type:"displayfield",
			labelWidth: 0,emptyText: '模板备注',
			style: "white-space:normal; word-break:break-all; ",
			name: 'EMaintenanceData_TempletMemo',value:val,
			itemId: 'EMaintenanceData_TempletMemo',
			colspan : 2,
			width : me.defaults.width * 2
		};
		return obj;
	},

    /*创建单选组组件*/
	createRadioGroup: function(fieldLabel, i, DefaultValue,ItemList,cols) {
		var me = this;
		if(!cols)cols=2;
		var rb='rb'+i;
		var obj = {
			xtype: 'uxRadioGroup',
			cols: cols,
			style:"text-align:center",
			vertical: true,
			rb:rb,
			width: me.defaults.width * 1,
			itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i,
			ItemList:ItemList,
			DefaultValue:DefaultValue
		};
		return obj;
	},
	
    /*创建复选框组件*/
	createcheckboxGroup: function(fieldLabel, i, DefaultValue,ItemList,columns) {
		var me = this;
		if(!columns)columns=2;
		var obj = {
			fieldLabel: fieldLabel,
			xtype: 'uxcheckboxgroup',
			columns: columns,
			vertical: true,
			width: me.defaults.width * 1,
			itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i,
			items:ItemList
		};
		return obj;
	},
	/*创建状态单选组件(复选框单选)*/
	createStatus: function(fieldLabel, i,DefaultValue,NameText1,NameText2) {
		var me = this,val=false;
		var obj = {
			xtype: 'uxcheckbox',
			NameText1:NameText1,
			NameText2:NameText2,
			width: me.defaults.width * 1,
			itemId: 'EMaintenanceData_ItemResult' + i,
			name: 'EMaintenanceData_ItemResult' + i
		};
		if(DefaultValue=='1'){
			/*默认选中 1 正常*/
			obj.defalutVal1=true;
		}
		if(DefaultValue=='2'){		
			/*默认选中 异常*/
			obj.defalutVal2=true;
		}
		return obj;
	},
	
	/**
	 * 数据保存
	 */
	SaveMaintenanceData: function(obj2, TempletID,type,operatedate,ItemMemo,isTbAdd,BatchNumber) {
		var me = this,entityJSON=[],isExect=false;
		var i = 0,ItemDataType = 'C';
        var OperateTime = JcallShell.Date.toString(operatedate);
        //IsSpreadItemList 展开列表 (单选组)
		var ItemResult = null,text = '',ptext='',pid = '',tid = '',
		    TempletType = '',IsSpreadItemList=null,IsMultiSelect=null;
		var result = Ext.isArray(obj2); //为数组时才处理
		
		if(result) {
			Ext.Array.each(obj2, function(model) {
				ItemResult = null;
				if(model['ItemDataType']) {
					ItemDataType = model['ItemDataType'];
				} else {
					ItemDataType = 'C';
				}
				text = model['text'];
				text=text.replace(/[\\r\\n\\s]/g, '');
				ptext=model['ptext'];
				ptext=ptext.replace(/[\\r\\n\\s]/g, '');
				pid = model['pid'];
				tid = model['tid'];
				TempletType = ptext;
				IsSpreadItemList=model['IsSpreadItemList'];
				IsMultiSelect=model['IsMultiSelect'];
				var Id = me.getComponent('EMaintenanceData_Id' + i).getValue();
				if(me.getComponent('EMaintenanceData_ItemResult' + i)) {
					ItemResult = me.getComponent('EMaintenanceData_ItemResult' + i).getValue();
				}
				switch(ItemDataType) {
					case 'D':
						if(ItemResult) {
							ItemResult = JcallShell.Date.toString(ItemResult,true);
						}
						break;
					case 'T':
						if(ItemResult) {
							ItemResult = me.getTimeData(ItemResult);
						}
						break;
					case 'DT':
					   
						break;
					case 'E':
					    //复选框选择和下拉列表(0,1)
						if(ItemResult == true) {
							ItemResult = '1';
						} else{
							ItemResult = '0';
						}
						//展开(单选组)
						if(IsSpreadItemList && IsSpreadItemList=='1'){
							var rb='rb'+i;
							ItemResult = me.getForm().findField(rb).getGroupValue(); //此处获取到的是inputValue的值
						}
						break;
					case 'S':
					    if(!ItemResult) ItemResult='0';
					    break;
					case 'CL'://下拉框

					    //下拉多选,需要解析数组
					    if((!IsSpreadItemList || IsSpreadItemList=='0') && IsMultiSelect=='1'){
					    	if(ItemResult.length==0){
					    		ItemResult='';
					    	}else{
					    		ItemResult=ItemResult.toString();
					    	}
					    }
					    break;
					default:
						break;
				}
				var entity = {
					ItemResult: ItemResult,
					TempletItem: text,
					TempletType: TempletType,
					TempletTypeCode: pid,
					TempletItemCode: tid,
					TempletDataType: 2,
					ItemDataType: ItemDataType,
					ItemDate:JShell.Date.toServerDate(operatedate),
					OperateTime:JShell.Date.toServerDate(OperateTime),
					ItemMemo:ItemMemo
				};
				if(Id){
					entity.Id=Id; 
				}
				 //当前是列表类型时,使用TB区分
			    if(type.length == 2 && type.substr(0, 1).toUpperCase() == "T"){
			    	type='TB';
			    }
				if(type=='TB' &&  (isTbAdd==1 || isTbAdd=='1')){
					entity.BatchNumber=BatchNumber;
				}
				//模板
				if(TempletID != '') {
					entity.ETemplet = {
						Id: TempletID,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					};
				}
				//默认员工ID
				var UserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				//默认员工名称
				var UserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
				if(UserID != '') {
					entity.HREmployee = {
						Id: UserID,
						CName:UserName,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					};
				}
				entityJSON.push(entity);
				i = i + 1;
			});
		}		
		return entityJSON;
	},
	/**
	 * 验证时间格式
	 */
	getTimeData: function(ItemResult) {
		var me = this;
	   var operatedate2 = JcallShell.Date.toString(ItemResult);
		Result	= me.toString(operatedate2);
		return Result;
	},
	/**获取时间字符串
	 *返回时分秒
	 */
	toString: function(value) {
		var v = JcallShell.Date.getDate(value);
		if (!v) return null;

		var info = '',
			year = v.getFullYear() + '',
			month = (v.getMonth() + 1) + '',
			date = v.getDate() + '';

		month = month.length == 1 ? '0' + month : month;
		date = date.length == 1 ? '0' + date : date;
		var hours = v.getHours() + '',
			minutes = v.getMinutes() + '',
			seconds = v.getSeconds() + '';

		hours = hours.length == 1 ? '0' + hours : hours;
		minutes = minutes.length == 1 ? '0' + minutes : minutes;
		seconds = seconds.length == 1 ? '0' + seconds : seconds;

		info += ' ' + hours + ':' + minutes + ':' + seconds;
		return info;
	},
	/**更改时间*/
	changeDate: function(val) {
		var me = this;
		var Date = me.getComponent('buttonsToolbar').getComponent('EMaintenanceData_Date');
	    if(me.ISEDITDATE=='false'){
	    	Date.setValue(val);
	    }else{
	    	Date.setText(val);
	    }
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		this.getForm().reset();
	},
	/**根据模板ID查看填表说明*/
	showInfoById:function(id){
		var me = this;
		JShell.Win.open('Shell.class.qms.equip.templet.operate.MenoPanel', {
			resizable: false,
			maximizable: false, //是否带最大化功能
			width: 450,
	        height:215,
			PK:id
		}).show();
	},
	/**
	 * 取出数据设置单选组
	 * E、S 只有两项
	 * E inputValue值 只能是0和1（第一个值是1第二个值是0）
	 * S inputValue值 只能是1和2（第一个值是1第二个值是2）
	 * CL 
	 */
	getRadioList: function(ItemValueList,type,radioname) {
		var me = this,
			arr = [],obj = {},len=0;     
		var ValueList = ItemValueList.split('#');
		len=ValueList.length;
		if(len==0)return;
		var num=1;
		for(var i = 0; i < len; i++) {
			obj={};
			num=num-i;
           	obj.boxLabel=ValueList[i];
           	obj.name=radioname;
           	if(type=='E'){//先1后0
           		obj.inputValue=num;
           	}
           	if(type=='S'){//先1后2
           		obj.inputValue=i+1;
           	}
           	if(type=='CL'){
           		obj.inputValue=ValueList[i];
           	}
           	obj.style= {
	            margin: '0px 10px 0px 0px'
	        };
			arr.push(obj);
		};
		return arr;
	},
	/**
	 * 动态创建S组件
	 */
	createSfield:function(ItemDataType,ItemValueList,IsSpreadItemList,index,DefaultValue,DataLength){
		var me=this,execute={};
		 /**[MD1|管路是否泄漏|E|1|是#否|1] 输入界面出现展开列表    IsSpreadItemList=1 展开列表 单选组*/
	    if(ItemValueList){
	    	 /**下拉框 [MD1|管路是否泄漏|E|1|是#否] 输入界面出现下拉列表   是#否 存在 ItemValueList */
		    if(IsSpreadItemList == '0'){
		    	var comboStore2=me.getSList(ItemValueList);
		        execute = me.createComText(' ', index, comboStore2, DefaultValue, DataLength);
		    }else{
		    	if(!DefaultValue)DefaultValue='0';
		        var SRadioText=me.getSRadioText(ItemValueList);
		        //获取对应的显示值
		    	execute = me.createStatus(' ', index,DefaultValue,SRadioText[0],SRadioText[1]);
		    }
	    }else{	    	
	    	/**单选组 没有设置值，默认显示为正常,异常*/
	    	if(!DefaultValue)DefaultValue='0';
	        //获取对应的显示值
	    	execute = me.createStatus(' ', index,DefaultValue,'正常','异常');
	    }
	    return execute;
	},
	/**
	 * 动态创建E组件
	 */
	createefield:function(ItemDataType,ItemValueList,IsSpreadItemList,index,DefaultValue,DataLength){
		var me=this,execute={};
		 /**[MD1|管路是否泄漏|E|1|是#否|1] 输入界面出现展开列表    IsSpreadItemList=1 展开列表 单选组*/
	    if(ItemValueList){
	    	 if(IsSpreadItemList == '0'){
		    	var comboStore2=me.getcheckBoxItemValueList(ItemValueList);
		        execute = me.createComText(' ', index, comboStore2, DefaultValue, DataLength);
		    }else{
		    	var radioname='rb'+index;
		    	if(!DefaultValue)DefaultValue='0';
		        var RadioList=me.getRadioList(ItemValueList,ItemDataType,radioname);
		    	execute = me.createRadioGroup(' ', index,DefaultValue,RadioList);
		    }

	    }else{
	    	//复选框选择
	    	execute = me.createCheckbox(' ', index,DefaultValue);
	    }
	    return execute;
	},
		/**
	 * 取出数据设置单选组
	 * 只有两个复选框,
	 */
	getSRadioText: function(ItemValueList) {
		var me = this,
			arr = [],radioname = {};     
		var ValueList = ItemValueList.split('#');
		if(ItemValueList.length=='0')return;
		for(var i = 0; i < 2; i++) {
           	arr.push(ValueList[i]);
		};
		return arr;
	},
	/**
	 * 设置E类型的下拉框（是和否）
	 */
	getcheckBoxItemValueList: function(ItemValueList) {
		var me = this,
			arr = [],
			obj = {},
			list = [];
		var ValueList = ItemValueList.split('#');
		var num=1,strval='';
		for(var i = 0; i < 2; i++) {
			num=num-i;
			strval=	""+num;
			list = [strval, ValueList[i]];
			arr.push(list);
		};
		return arr;
	},
	/**
	 * 动态创建CL组件
	 */
	createCLfield:function(ItemDataType,ItemValueList,IsMultiSelect,IsSpreadItemList,IsInputItemValue,index,DefaultValue,comboStore,MaxValue, MinValue,MaxDataLength){
		var me=this,comobo={};
		var SelectType='',editable=false;
		var radioname='rb'+index;
	    //不允许多选
	    if(!IsMultiSelect || IsMultiSelect=='0' ){
	    	IsMultiSelect='0';
	    }
	    //不允许展开
	    if(!IsSpreadItemList || IsSpreadItemList=='0' ){
	    	IsSpreadItemList='0';
	    }
	     //不允许编辑
	    if(!IsInputItemValue || IsInputItemValue=='0' ){
	    	IsInputItemValue='0';
	    }
	    //多选并且展开使用复选组
	    if(IsMultiSelect=='1' && IsSpreadItemList=='1' ){
	    	SelectType='A';
	    }
	     //单选并且展开使用单选组
	    if(IsMultiSelect=='0' && IsSpreadItemList=='1' ){
	    	SelectType='B';
	    }
	    //不展开但使用多选
	    if(IsMultiSelect=='1' && IsSpreadItemList=='0'){
	    	SelectType='C';
	    }
        switch(SelectType){
	    	case 'A':
		        var comList=me.getRadioList(ItemValueList,ItemDataType,radioname);
		    	comobo = me.createcheckboxGroup(' ', index,DefaultValue,comList,comList.length);
			break;
			case 'B':
		        var RadioList=me.getRadioList(ItemValueList,ItemDataType,radioname);
		        var cols=RadioList.length;
		    	comobo = me.createRadioGroup(' ', index,DefaultValue,RadioList,cols);
			break;
			case 'C':
			   	comobo = me.createMultiComboBox(' ', index, DefaultValue, MaxValue, MinValue,comboStore);
			break;
			default:
		        if(IsInputItemValue=='1') editable=true;
				comobo = me.createComText(' ', index, comboStore, DefaultValue, MaxDataLength,editable);
			break;
	    }
	    return comobo;
	},
	/**
	 * 默认值为0，值为1和2 1是正常 2是异常
	 * 设置S类型的下拉框（是和否）
	 */
	getSList: function(ItemValueList) {
		var me = this,
			arr = [],
			obj = {},
			list = [];
			
		var ValueList = ItemValueList.split('#');
		ValueList.splice(0, 0, '请选择');  
		var num=1,strval='';
		for(var i = 0; i <3; i++) {
		    var str=i+"";
			list = [str, ValueList[i]];
			arr.push(list);
		};
		return arr;
	},
	/**
	 * 设置下拉框
	 */
	getItemValueList: function(ItemValueList) {
		var me = this,
			arr = [],
			obj = {},
			list = [];
		var ValueList = ItemValueList.split('#');
		for(var i = 0; i < ValueList.length; i++) {
			list = [ValueList[i], ValueList[i]];
			arr.push(list);
		};
		return arr;
	},
	
	/**还原表单数据
	 * isDaily 是否加载前一天的数据
	 */
	SetFormData: function(list, ItemCode, Type, j,IsSpreadItemList,IsMultiSelect,isDaily,InitItemCode) {
		var me = this;
		var ItemDataType = '',
			TempletDataType = '',
			TempletItemCode = '',
			ItemResult = '',
			Id = '',
			TempletItem = '',
			ItemMemo = '';
		
		//清空表单信息 包括默认值
		if(!isDaily){
			me.getComponent('EMaintenanceData_Id' + j).setValue('');
		}
	    me.getComponent('EMaintenanceData_ItemResult' + j).setValue('');
		for(var i=0; i < list.length; i++) {
			TempletItem = list[i].EMaintenanceData_TempletItem;
			Id = list[i].EMaintenanceData_Id;
			ItemResult = list[i].EMaintenanceData_ItemResult+"";
			ItemMemo = list[i].EMaintenanceData_ItemMemo;
			ItemDataType = list[i].EMaintenanceData_ItemDataType;
			TempletDataType = list[i].EMaintenanceData_TempletDataType;
			TempletItemCode = list[i].EMaintenanceData_TempletItemCode;
		     
		    if(TempletItemCode == ItemCode  && TempletDataType == '2') {
                me.setItemResult(TempletItemCode,ItemResult,IsSpreadItemList);
				if(!isDaily){
					me.getComponent('EMaintenanceData_Id' + j).setValue(Id);
				}
				if(ItemResult){
					ItemResult = ItemResult.replace(/(^\s*)|(\s*$)/g, ""); 
				}
				me.getComponent('EMaintenanceData_ItemResult' + j).setValue(ItemResult);
				var Sysdate =JShell.System.Date.getDate();
		        //当前时间
		        var TDate = JShell.Date.toString(Sysdate, true);
		        if(ItemResult && InitItemCode=='1' && Type=='D'){
		        	if(ItemResult <= TDate ){
		        		me.getComponent('EMaintenanceData_ItemResult' + j).setFieldStyle('color:red;');
			        }
		        }
				continue;
			}
		}
	},
	
	setItemResult:function(ItemDataType,ItemResult,IsSpreadItemList){
		var me =this;
		switch(ItemDataType) {
			case 'C':
				var reg = new RegExp("</br>", "g");
                ItemResult = ItemResult.replace(reg, "\r\n");
			    break;
			case 'D':
				ItemResult = JcallShell.Date.toString(ItemResult, true);
				var val = JcallShell.Date.isValid(ItemResult);
				if(val == null) {
					ItemResult = '';
				}
				break;
			case 'T':
			   if(ItemResult){
			   	    ItemResult=ItemResult.replace(/(^\s*)|(\s*$)/g,"");
			   }
				break;
			case 'DT':
				var val = me.isValidDateTime(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'I':
				var val = me.isInteger(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'L':
				var val = me.isInteger(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'F':
				var val = me.isValidDecimal(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'E':

				break;
			case 'S':
				break;
			case 'CL':
			    //不展开 多选
			    if((!IsSpreadItemList || IsSpreadItemList=='0') && IsMultiSelect=='1'){
			       ItemResult = ItemResult.split(',');   
			    }
				break;
			default:
				break;
		}
		return ItemResult;
	},
	setResultValuse:function(ItemDataType,ItemResult){
		var me =this;
		switch(ItemDataType) {
			case 'C':
				var reg = new RegExp("</br>", "g");
                ItemResult = ItemResult.replace(reg, "\r\n");
			    break;
			case 'D':
				ItemResult = JcallShell.Date.toString(ItemResult, true);
				var val = JcallShell.Date.isValid(ItemResult);
				if(val == null) {
					ItemResult = '';
				}
				break;
			case 'T':
			   if(ItemResult){
			   	    ItemResult=ItemResult.replace(/(^\s*)|(\s*$)/g,"");
			   }
				break;
			case 'DT':
				var val = me.isValidDateTime(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'I':
				var val = me.isInteger(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'L':
				var val = me.isInteger(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'F':
				var val = me.isValidDecimal(ItemResult);
				if(val == false) {
					ItemResult = '';
				}
				break;
			case 'E':

				break;
			case 'S':
				break;
//			case 'CL':
//			    //不展开 多选
//			    if((!IsSpreadItemList || IsSpreadItemList=='0') && IsMultiSelect=='1'){
//			       ItemResult = ItemResult.split(',');   
//			    }
//				break;
			default:
				break;
		}
		return ItemResult;
	},
     /**
	 * 动态创建表单组件
	 */
    createFormCom: function(list, index, model,type,TempletID,OperateDate,InitItemCode) {
	    var me = this;
	    var ItemDataType = 'C',
			DefaultValue = '',MinValue = 0,MaxDataLength =500,
			MaxValue = 1000000,allowDecimals = true,
			DecimalLength = 0,ItemValueList = null,IsSpreadItemList=null,
			ItemWidth=null,ItemHeight=null,ItemCode=null,
			IsMultiSelect=null,IsInputItemValue=null,AddValue=null;
	    if(model.ItemDataType) {
		 	ItemDataType = model.ItemDataType;
		}
		if(model.MaxDataLength) {
			MaxDataLength = model.MaxDataLength;
		}
		if(model.DecimalLength) {
			DecimalLength = model.DecimalLength;
		}
		if(model.DefaultValue) {
			DefaultValue = model.DefaultValue;
		}
		if(model.ItemValueList) {
			ItemValueList = model.ItemValueList;
			comboStore = me.getItemValueList(ItemValueList);
		}
		//是否展开 1为展开，0为下拉（默认）
		if(model.IsSpreadItemList){
			IsSpreadItemList = model.IsSpreadItemList;
		}
		//1为可以选择多个列表项的值，0为只选择其中一项（默认）
		if(model.IsMultiSelect){
			IsMultiSelect = model.IsMultiSelect;
		}
		//可输入结果：1为可手工输入结果，0为只能从列表项中选择结果）
		if(model.IsInputItemValue){
			IsInputItemValue = model.IsInputItemValue;
		}
		if(model.MaxValue) {
			MaxValue = model.MaxValue;
		}
		if(model.MinValue) {
			MinValue = model.MinValue;
		}
		if(model.ItemWidth) {
			ItemWidth = model.ItemWidth;
		}
		if(model.ItemHeight) {
			ItemHeight = model.ItemHeight;
		}
		if(model.AddValue){
           AddValue = model.AddValue.replace(/\\n/g, '');
		}
		if(model.ItemCode){
			ItemCode = model.ItemCode;
		}
		switch(ItemDataType) {
			case 'D':
				var Date = me.createDate(' ', index, DefaultValue,InitItemCode);
				list.push(Date);
				break;
			case 'T':
				var Time = me.createTime(' ', index, DefaultValue);
				list.push(Time);
				break;
			case 'DT':
				var DataTime = me.createDataTime(' ', index,DefaultValue);
				list.push(DataTime);
				break;
			case 'C':
				var Text = me.createText(' ', index, DefaultValue, MaxDataLength, MaxValue, MinValue,ItemHeight,ItemWidth,type);
				list.push(Text);
				break;
			case 'CL':
                var comobo=me.createCLfield(ItemDataType,ItemValueList,IsMultiSelect,IsSpreadItemList,IsInputItemValue,index,DefaultValue,comboStore,MaxValue, MinValue,MaxDataLength);
                list.push(comobo)
				break;
			case 'E':
			      if(!DefaultValue)DefaultValue='0';
			      var execute = me.createefield(ItemDataType,ItemValueList,IsSpreadItemList,index,DefaultValue,MaxDataLength);
			      list.push(execute);	
				break;
		  	case 'E1':
                var StatusList2= [['1', '已执行'],['2', '不执行']];
				var execute1 = me.createComboBox(' ', index, DefaultValue, MaxValue, MinValue,StatusList2);
				list.push(execute1);
				break;
			case 'S':			    
			    if(!DefaultValue)DefaultValue='0';
			    var status = me.createSfield(ItemDataType,ItemValueList,IsSpreadItemList,index,DefaultValue,MaxDataLength);
			    list.push(status);
				break;
			case 'S1':
			    var StatusList= [['0', ''],['1', '正常'],['2', '异常']];
				var Status = me.createComboBox(' ', index, DefaultValue, MaxValue, MinValue,StatusList);
				list.push(Status);
				break;
			case 'I':
				var Number = me.createNumber(' ', index, DefaultValue, MaxValue, MinValue, DecimalLength, false,TempletID,AddValue,ItemCode,OperateDate);
				list.push(Number);
				break;
			case 'L':
				var LogNumber = me.createNumber(' ', index, DefaultValue, MaxValue, MinValue, DecimalLength, false,TempletID,AddValue,ItemCode,OperateDate);
				list.push(LogNumber);
				break;
			case 'F':
				var floatData = me.createNumber(' ', index, DefaultValue, MaxValue, MinValue, DecimalLength, allowDecimals,TempletID,AddValue,ItemCode,OperateDate);
				list.push(floatData);
				break;
			default:
				var Text = me.createText(' ', index, DefaultValue, 40, MaxValue, MinValue);
				list.push(Text);
				break;
		}
		return list;
	},
	/**验证是否是整型*/
	isInteger: function(str) {
		if(str % 1 === 0) {
			return true;
		} else {
			return false;
		}
	},
	/**验证是长时间*/
	isValidDateTime: function(str) {
		var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
		var r = str.match(reg);
		if(r == null) return false;
		var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6], r[7]);
		return(d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6] && d.getSeconds() == r[7]);
	},
	/**验证断时间*/
	isValidTime: function(time) {
		var a = time.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
		if(a) {
			return true;
		} else {
			return false;
		}
		if(a[1] > 24 || a[3] > 60 || a[4] > 60) {
			return false
		}
	},
	/**验证浮点型*/
	isValidDecimal: function(str) {
		if(this.isInteger(str)) return true;
		var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
		if(re.test(str)) {
			if(RegExp.$1 == 0 && RegExp.$2 == 0) return false;
			return true;
		} else {
			return false;
		}
	},
	/**根据TempletItemCode匹配数据*/
    setDailyData:function(list){
    	var me =this;
    	var arr =[],itemArr=[];
    	for(var i=0; i<me.items.items.length;i++){
    		var strItemId=me.items.items[i].itemId;
    		if(strItemId.indexOf("EMaintenanceData_TempletItemCode") != -1 ){
    			itemArr.push(strItemId);
    		}
    	}
    	for(var j=0;j<itemArr.length;j++){
    		var TempletItem= me.getComponent('EMaintenanceData_TempletItemCode' + j);
			var textVal= TempletItem.getFieldLabel();
            for(var i =0 ;i<list.length;i++){
                var  TempletDataType=list[i].EMaintenanceData_TempletDataType;
                if(TempletDataType!='1'){
                	var ItemText=list[i].EMaintenanceData_TempletItemCode;
		            if(textVal==ItemText){
						var ItemDataType=list[i].EMaintenanceData_ItemDataType;
		    			var ItemResult =list[i].EMaintenanceData_ItemResult;
		    			var val = me.setResultValuse(ItemDataType,ItemResult);
		    			me.getComponent('EMaintenanceData_ItemResult' + j).setValue(val);
					}
                }
    	    }
		}
    },
    /**清空结果值,不清空id*/
    clearResultData:function(){
    	var me =this;
    	var arr =[],itemArr=[];
    	for(var i=0; i<me.items.items.length;i++){
    		var strItemId=me.items.items[i].itemId;
    		if(strItemId.indexOf("EMaintenanceData_TempletItemCode") != -1 ){
    			itemArr.push(strItemId);
    		}
    	}
    	for(var j=0;j<itemArr.length;j++){
    		me.getComponent('EMaintenanceData_ItemResult' + j).setValue('');
    	}
    },
    /**根据参数控制载入数据按钮是否显示*/
	getEParaVal: function(callback) {
		var me = this;	
		var paraVal =0;
	    var url = JShell.System.Path.getRootUrl(me.selectParaUrl);
	    var fields='EParameter_ParaValue';
		var where="&fields="+fields+"&where=ParaType='QualityRecord' and ParaNo = 'IsShowLoadDataButton')";
		url+=where;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var obj = data.value;
				if(callback) callback(obj);
			} else {
				JShell.Msg.error('获取系统参数出错！' + data.msg);
			}
		}, false);
	},
	/**还原表单数据
	 * isDaily 是否加载前一天的数据
	 */
	SetFormData2: function(list, ItemCode, Type, j,IsSpreadItemList,IsMultiSelect,isDaily) {
		var me = this;
		var ItemDataType = '',
			TempletDataType = '',
			TempletItemCode = '',
			ItemResult = '',
			Id = '',
			TempletItem = '',
			ItemMemo = '';
		for(var i=0; i < list.length; i++) {
			TempletItem = list[i].EMaintenanceData_TempletItem;
			Id = list[i].EMaintenanceData_Id;
			ItemResult = list[i].EMaintenanceData_ItemResult+"";
			ItemMemo = list[i].EMaintenanceData_ItemMemo;
			ItemDataType = list[i].EMaintenanceData_ItemDataType;
			TempletDataType = list[i].EMaintenanceData_TempletDataType;
			TempletItemCode = list[i].EMaintenanceData_TempletItemCode;
		    if(TempletItemCode == ItemCode  && TempletDataType == '2') {
				me.getComponent('EMaintenanceData_Id' + j).setValue(Id);
				continue;
			}
		}
	},
	changeDailyBtn : function(bo){
		var  me = this;
		var dailybtn2 = me.getComponent('buttonsToolbar').getComponent('dailybtn2');
	    dailybtn2.setDisabled(bo);
	}
});