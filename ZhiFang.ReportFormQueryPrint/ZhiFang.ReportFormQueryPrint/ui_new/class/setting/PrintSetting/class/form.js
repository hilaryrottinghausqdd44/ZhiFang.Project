Ext.define('Shell.class.setting.PrintSetting.class.form',{
    extend: 'Shell.ux.panel.AppPanel',
    formType:'',

	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.formType =='update'){
			me.getComponent("SPID").setValue(me.record[0].data.SPID);
			me.getComponent("CName").setValue([me.record[0].data.SectionNo]);
			me.getComponent("PrintFormat").setValue(me.record[0].data.PrintFormat);
			me.getComponent("PrintProgram").setValue(me.record[0].data.PrintProgram);
			me.getComponent("DefPrinter").setValue(me.record[0].data.DefPrinter);
			me.getComponent("TestItemNo").setValue(me.record[0].data.TestItemNo);
			me.getComponent("ItemCountMin").setValue(me.record[0].data.ItemCountMin);
			me.getComponent("ItemCountMax").setValue(me.record[0].data.ItemCountMax);
			me.getComponent("SickTypeNo").setValue(me.record[0].data.sicktypeno);
			me.getComponent("PrintOrder").setValue(me.record[0].data.PrintOrder);
			me.getComponent("microattribute").setValue(me.record[0].data.microattribute);
			me.getComponent("IsRFGraphdataPDf").setValue(me.record[0].data.IsRFGraphdataPDf);
		}
	},

	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var str = "";
		//小组类别下拉框数据
		var states = Ext.create('Ext.data.Store', {
			    fields: ['abbr', 'name'],
			    data : [
			        {"abbr":"全部", "name":"全部"},
			        {"abbr":"阴性报告", "name":"阴性报告"},
			        {"abbr":"阳性报告", "name":"阳性报告"}
			    ]
			});
		var IsRFGraphdataPDfStates = Ext.create('Ext.data.Store', {
		    fields: ['abbr', 'name'],
		    data : [
		        {"abbr":"false", "name":"false"},
		        {"abbr":"true", "name":"true"}
		    ]
		});
			Ext.define('User', {
			    extend: 'Ext.data.Model',
			    fields: [
			        {name: 'CName', type: 'string'},
			        {name: 'SectionNo', type: 'int'}
			    ]
			});
			
			var myStore = Ext.create('Ext.data.Store', {
			    fields: ["CName",'SectionNo'],
			    autoLoad:true,
			    proxy: {
			        type: 'ajax',
			        url: Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetPGroup?fields=SectionNo,CName',
			        reader: {
			            type: 'json',
			            root:'list'
			        },
			        extractResponseData:function(response){
			         	var result = Ext.JSON.decode(response.responseText);
			         	return me.responseToList(response);//return result.ResultDataValue;
			        }
			    }
			});
			var sicktypestore = Ext.create('Ext.data.Store', {
			    fields: ["CName",'SickTypeNo'],
			    autoLoad:true,
			    proxy: {
			        type: 'ajax',
			        url: Shell.util.Path.rootPath+'/ServiceWCF/DictionaryService.svc/GetSickType?fields=SickTypeNo,CName',
			        reader: {
			            type: 'json',
			            root:'list'
			        },
			        extractResponseData:function(response){
			         	var result = Ext.JSON.decode(response.responseText);
			         	//console.log(result);
			         	//return me.responseToList(response);
			         	//console.log(Ext.JSON.decode(result.ResultDataValue).rows);
			         	return Ext.JSON.decode(result.ResultDataValue).rows;
			        }
			    }
			});

		me.items = [
							
							{
								xtype:"textfield",itemId:"SPID",width:350,
								style:"margin-left:20px",name:"SPID",fieldLabel:"id",
								hidden:true
							},
							{
								xtype:"combo",itemId:"CName",width:350,editable:false,
								style:"margin-top:10px;margin-left:20px",name:"CName",
								fieldLabel:"小组名称",store: myStore,//Mode: 'local',
								displayField: 'CName',valueField:'SectionNo'
							},
							{
								xtype:"textfield",itemId:"PrintFormat",width:350,
								style:"margin-left:20px",name:"PrintFormat",fieldLabel:"打印格式"
							},
							{
								xtype:"textfield",itemId:"PrintProgram",width:350,
								style:"margin-left:20px",name:"PrintProgram",fieldLabel:"模板名称"
							},
							{
								xtype:"textfield",itemId:"DefPrinter",width:350,
								style:"margin-left:20px",name:"DefPrinter",fieldLabel:"默认打印机",
								value:"站点默认打印机"
							},
							{
								xtype:"textfield",itemId:"TestItemNo",width:350,
								style:"margin-left:20px",name:"TestItemNo",fieldLabel:"特殊项目号"
							},
							{
								xtype:"numberfield",itemId:"ItemCountMin",width:350,
								style:"margin-left:20px",name:"ItemCountMin",fieldLabel:"项目最小数量"
							},
							{
								xtype:"numberfield",itemId:"ItemCountMax",width:350,
								style:"margin-left:20px",name:"ItemCountMax",fieldLabel:"项目最大数量"
							},
							{
								xtype:"combo",itemId:"SickTypeNo",width:350,editable:false,
								style:"margin-top:10px;margin-left:20px",name:"SickTypeNo",
								fieldLabel:"就诊类型",store: sicktypestore,
								displayField: 'SickTypeNo',valueField:'SickTypeNo'
							},
							{
								xtype:"numberfield",itemId:"PrintOrder",width:350,
								style:"margin-left:20px",name:"PrintOrder",fieldLabel:"打印排序",
								value:"1"
								
							},
							{
								xtype:"combobox",itemId:"microattribute",width:350,editable:false,
								style:"margin-top:10px;margin-left:20px",
								name:"Microattribate",fieldLabel:"微生物属性",
								store: states,queryMode: 'local',displayField: 'name',valueField: 'abbr'
							},
							{
								xtype:"combobox",itemId:"IsRFGraphdataPDf",width:350,editable:false,
								style:"margin-top:10px;margin-left:20px",
								name:"IsRFGraphdataPDf",fieldLabel:"是否外送单",
								store: IsRFGraphdataPDfStates,queryMode: 'local',displayField: 'name',valueField: 'abbr'
							},
							{
								xtype:"button",itemId:"btn",width:200,
								style:"margin-left:100px;margin-top:10px",name:"submitBtn",text:"提交",
								handler:function(){
									//添加、修改保存事件
									var SectionNo=me.getComponent("CName").value;
									var SPID=me.getComponent("SPID").value;
									var	PrintFormat=me.getComponent("PrintFormat").value;
									var	PrintProgram=me.getComponent("PrintProgram").value;
									var	DefPrinter=me.getComponent("DefPrinter").value;
									var	TestItemNo=me.getComponent("TestItemNo").value;
									var	ItemCountMin=me.getComponent("ItemCountMin").value;
									var	ItemCountMax=me.getComponent("ItemCountMax").value;
									var	SickTypeNo=me.getComponent("SickTypeNo").value;
									var	PrintOrder=me.getComponent("PrintOrder").value;
									var	microattribute=me.getComponent("microattribute").value;
									var	IsRFGraphdataPDf=me.getComponent("IsRFGraphdataPDf").value;
									//数据不为空
									if(PrintFormat != "" && PrintProgram != ""){
										var str = "";//是否成功
										var entity = {
													"SectionNo":SectionNo,
													"SPID":SPID?SPID:0,
													"PrintFormat": PrintFormat, 
													"PrintProgram": PrintProgram, 
													"DefPrinter" : DefPrinter,
													"TestItemNo": TestItemNo ? TestItemNo : null,
													"ItemCountMin": ItemCountMin? ItemCountMin : null,
													"ItemCountMax": ItemCountMax? ItemCountMax : null,
													"sicktypeno":SickTypeNo?SickTypeNo:null,
													"PrintOrder": PrintOrder,
													"IsRFGraphdataPDf": IsRFGraphdataPDf,
													//"microattribute": microattribute
													
										};
										if(microattribute != '全部' || microattribute != null){
											entity.microattribute= microattribute;
										}
										if(me.formType == "add"){
											Ext.Ajax.defaultPostHeader = 'application/json';
											Ext.Ajax.request({
											method: 'post',
										    url: Shell.util.Path.rootPath +'/ServiceWCF/DictionaryService.svc/AddSectionPrint',
									        params:Ext.JSON.encode({entity:entity}),
										    success: function(response){
										        str = Ext.JSON.decode(response.responseText);
										        me.fireEvent("save",me,str);
										    }
										});
										}else if(me.formType == "update"){
											Ext.Ajax.defaultPostHeader = 'application/json';
											Ext.Ajax.request({
											method: 'post',
										    url: Shell.util.Path.rootPath +'/ServiceWCF/DictionaryService.svc/UpdateSectionPrint',
									        params:Ext.JSON.encode({entity:entity}),
										    success: function(response){
										        str = Ext.JSON.decode(response.responseText);
										        me.fireEvent("save",me,str);
										    }
										});
										};
										
									}else{
										Ext.MessageBox.alert("操作提示","数据不能为空！");
									}
								}
							}
						]
		;
		return me.items;
	}

});
