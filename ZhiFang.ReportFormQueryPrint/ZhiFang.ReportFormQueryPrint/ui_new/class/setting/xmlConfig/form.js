Ext.define("Shell.class.setting.xmlConfig.form", {
    extend: 'Shell.ux.panel.AppPanel',
    formType:'',

	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.formType =='update'){
			me.getComponent("ReportType").setValue(me.record[0].data.ReportType);
			me.getComponent("XSLName").setValue(me.record[0].data.XSLName);
			me.getComponent("PageName").setValue(me.record[0].data.PageName);
			me.getComponent("Name").setValue(me.record[0].data.Name);
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
			        {"abbr":"Normal", "name":"生化类"},
			        {"abbr":"NormalIncImage", "name":"生化类(图)"},
			        {"abbr":"Micro", "name":"微生物"},
			        {"abbr":"MicroIncImage", "name":"微生物(图)"},
			        {"abbr":"CellMorphology", "name":"细胞形态学"},
			        {"abbr":"FishCheck", "name":"Fish检测(图)"},
			        {"abbr":"SensorCheck", "name":"院感检测(图)"},
			        {"abbr":"ChromosomeCheck", "name":"染色体检测(图)"},
			        {"abbr":"PathologyCheck", "name":"病理检测(图)"}
//			        {"abbr":"TestGroupMicroSmear", "name":"TestGroupMicroSmear"},
//			        {"abbr":"TestGroupMicroSmearExt", "name":"TestGroupMicroSmearExt"},
//			        {"abbr":"TestGroupMicroCultureAssayAntibioticSusceptibility", "name":"TestGroupMicroCultureAssayAntibioticSusceptibility"},
//			        {"abbr":"TestGroupMicroCultureAssayAntibioticSusceptibilityExt", "name":"TestGroupMicroCultureAssayAntibioticSusceptibilityExt"}
			    ]
			});
		me.items = [
							{xtype:"combo",itemId:"ReportType",width:350,editable:false,style:"margin-top:10px;margin-left:20px",name:"ReportType",fieldLabel:"小组类别",store: states,queryMode: 'local',displayField: 'name',valueField: 'abbr'},
							{xtype:"textfield",itemId:"XSLName",width:350,style:"margin-left:20px",name:"XSLName",fieldLabel:"模板名称"},
							{xtype:"textfield",itemId:"PageName",width:350,disabled:true,labelStyle:"opacity:1",style:"margin-left:20px",name:"PageName",fieldLabel:"页面名称",value:'TechnicianPrint1.aspx'},
							{xtype:"textfield",itemId:"Name",width:350,style:"margin-left:20px",name:"Name",fieldLabel:"样式名"},
							//{xtype:"textfield",itemId:"ln",width:350,style:"margin-left:20px",name:"lastName",fieldLabel:"lastName"},
							{xtype:"button",itemId:"btn",width:200,style:"margin-left:100px;margin-top:10px",name:"submitBtn",text:"提交",
								handler:function(){
									//添加、修改保存事件
									var ReportType = me.getComponent("ReportType").value;
									var XSLName = me.getComponent("XSLName").value;
									var PageName = me.getComponent("PageName").value;
									var Name = me.getComponent("Name").value;
									//格式化数据
									ReportType = ReportType == null ? "" : ReportType;
									XSLName = XSLName == undefined ? "" : Ext.String.trim(XSLName);
									PageName = PageName == undefined ? "" : Ext.String.trim(PageName);
									Name = Name == undefined ? "" : Ext.String.trim(Name);
									//数据不为空
									if(ReportType != "" && XSLName != "" && PageName != "" && Name != ""){
										//var lastName = me.getComponent("ln").value;
										var store = me.getStore;
										var list = [];
										var str = "";//是否成功
										store.each(function (record) {
									        list.push({
									            "ReportType": record.get("ReportType"),
									            "XSLName": record.get("XSLName"),
								                "PageName": record.get("PageName"),
								                "Name": record.get("Name")//,
								                //"lastName": record.get("lastName")
								            });
								        });
										var data = {"ReportType": ReportType, "XSLName": XSLName, "PageName": PageName, "Name": Name};
										if(me.formType == "add"){
											list.push(data);
										}else if(me.formType == "update"){
											var record = me.record;
											//删除指定数据 得到剩余数据数组 list
									        for(var i = 0;i<list.length;i++){
									    		if(i == record[0].index){
									        		list[i] = data;
									        	}
									        }
										}
										list = me.toEng(list);
										var config = {
								            "?xml": {"@version":"1.0","@encoding":"utf-8"},
								            "DataSet": { "ReportFromShowXslConfig": list }
								       	};
								        config = Ext.JSON.encode(config);
										Ext.Ajax.defaultPostHeader = 'application/json';
										Ext.Ajax.request({
											method: 'POST',
										    url: Shell.util.Path.rootPath +'/ServiceWCF/ReportFormService.svc/SaveConfig',
									        params:Ext.JSON.encode({
									        	"fileName": "ReportFromShowXslConfig.xml",
	            								"configStr": config
									        }),
										    success: function(response){
										        str = Ext.JSON.decode(response.responseText);
										        me.fireEvent("save",me,str);
										    }
										});
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