/**
 * 项目表格设置
 * @author Jcall
 * @version 2020-01-06
 */
Ext.define('Shell.class.lts.sample.result.sample.TableConfig', {
	extend:'Shell.ux.form.Panel',
	requires:['Shell.class.lts.sample.result.sample.ColorField'],
	title:'项目显示设置',
	width:970,
	height:630,
	bodyPadding:'5px 5px 5px 10px',
	formtype:'edit',
	//布局
	layout:'anchor',
	defaults:{anchor:'100%'},
	//fieldset布局
	fieldsetLayout:{type:'table',columns:5},
	//fieldset内部组件初始属性
	fieldsetDefaults: {labelWidth:55,width:160,labelAlign:'right'},
	//显示宽度（英文字符）的默认宽度
	defaultColWidth:140,
	//显示位置的默认宽度
	defaultPositionWidth:140,
	//颜色的默认宽度
	defaultColorWidth:140,
	//默认值
	defaultData:{},
	//序号默认括号选择项
	BracketsList:[['【】','【】'],['()','()'],['[]','[]'],['','无']],
	//序号默认括号值
	defaultBrackets:'【】',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建内部组件
	createItems:function(){
		var me = this,
			items = [];
			
		//主键
		items.push({fieldLabel:'主键ID',name:'Id',hidden:true});
		//方块方式
		items.push(me.createBlockFieldSet());
		//项目名称
		items.push(me.createItemNameFieldSet());
		//显示内容
		items.push(me.creatContentFieldSet());
		//保存方式
		items.push(me.createSaveFieldSet());
		
    	return items;
	},
	//方块方式
	createBlockFieldSet:function(){
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;
			
		var fieldset = {
			xtype:'fieldset',title:'方块方式',
			collapsible:true,defaultType:'textfield',
			itemId:'BlockMode',layout:layout,defaults:defaults,
			items: [{
				xtype:'checkboxfield',margin:'0 5 0 5',
				boxLabel:'自动向右填充',name:'cbRightFill',
				itemId:'cbRightFill',checked:true,
				colspan:5,width:defaults.width * 3
			}, {
				xtype:'checkboxfield',margin:'0 5 0 5',
				boxLabel:'定列显示',name:'cbNumColShow',
				itemId:'cbNumColShow',colspan:1,
				width: defaults.width * 1,checked:false
			},{
				fieldLabel:'列数',name:'numCol',itemId:'numCol',
				xtype:'numberfield',minValue:1,
				value:1,colspan:1,width:140,
				style:{marginLeft:'-50px'}
			},{
				xtype:'radiogroup',fieldLabel:'',
				columns:2,vertical:true,colspan:4,
				width:defaults.width * 1+60,
				name:'rbBlockCol',itemId: 'rbBlockCol',
				style:{marginLeft:'10px'},
				items:[{
					boxLabel:'自动向右填充',name:'BlockColFill',inputValue:'1',checked:true
				},{
					boxLabel:'平均分配',name:'BlockColFill',inputValue:'2'
				 }]
			}]
		};
		
		return fieldset;
	},
	//项目名称
	createItemNameFieldSet:function(){
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;

		var fieldset = {
			xtype: 'fieldset',title:'项目名称',collapsible:true,
			defaultType:'textfield',itemId:'ItemNameFieldSet',
			layout:layout,defaults:defaults,
			items:[{
				xtype:'radiogroup',fieldLabel:'',
				columns:3,vertical:true,name:'rbItemName',itemId:'rbItemName',
				colspan:2,width:250,
				items:[{
					boxLabel:'项目简称',name:'ItemName',inputValue:'ItemSName',checked:true
				},{
					boxLabel:'中文名称',name:'ItemName',inputValue:'ItemName'
				},{
					boxLabel:'英文名称',name:'ItemName',inputValue:'ItemEName'
				}]
			}, {
				fieldLabel:'显示宽度',name:'ItemNameWidth',
                xtype:'numberfield',minValue:0,itemId:'ItemNameWidth',
				value:8,colspan:1,width:me.defaultColWidth
			},{
				fieldLabel:'显示位置',name:'ItemNamePosition',
				itemId:'ItemNamePosition',xtype:'numberfield',
				value:1,colspan: 2,width:me.defaultPositionWidth,
				style:{marginLeft:'20px'}
			}]
		};
		
		return fieldset;
	},
	//显示内容
	creatContentFieldSet:function(){
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;

		var fieldset = {
			xtype:'fieldset',title:'显示内容',
			collapsible:true,defaultType:'textfield',
			layout:layout,defaults:defaults,
			itemId:'ContentFieldSet',name:'ContentFieldSet',
			items: [{
				boxLabel:'序号',name:'cbNo',
				xtype:'checkboxfield',itemId:'cbNo',
				margin:'0 5 0 5',checked:false
			}, {
				fieldLabel:'显示宽度',name:'NoWidth',
				xtype:'numberfield',itemId:'NoWidth',
				colspan:1,width:me.defaultColWidth
			}, {
				fieldLabel:'显示位置',name:'NoPosition',
				xtype:'numberfield',itemId:'NoPosition',
				colspan:1,width:me.defaultPositionWidth,value:1
			},{
				fieldLabel:'括号',name:'NoContent',
				xtype:'uxSimpleComboBox',itemId:'NoContent',
				colspan:2,width:me.defaultColWidth,
				data:me.BracketsList,value:me.defaultBrackets
			},{
				boxLabel:'历史检验结果(灰色字体)',name:'cbHistoryResult',
				xtype:'checkboxfield',itemId:'cbHistoryResult',
				margin:'0 5 0 5',checked:true
			}, {
				fieldLabel:'显示宽度',name:'HistoryResultWidth',
				xtype:'numberfield',itemId:'HistoryResultWidth',
				colspan:1,width:me.defaultColWidth
			},{
				fieldLabel:'显示位置',name:'HistoryResultPosition',
				xtype:'numberfield',itemId:'HistoryResultPosition',
				colspan:1,width:me.defaultPositionWidth,value:3
			},{
				fieldLabel:'颜色',name:'HistoryResultColor',
				xtype:'colorfield',itemId:'HistoryResultColor',
				colspan:2,width:me.defaultColorWidth,
				allowBlank:false,value:'#808080'
			}, {
				boxLabel:'历史对比百分比',name:'cbHistoryPercentage',
				xtype:'checkboxfield',itemId:'cbHistoryPercentage',
				margin:'0 5 0 5',checked:false
			}, {
				fieldLabel:'显示宽度',name:'HistoryPercentageWidth',
				xtype:'numberfield',itemId:'HistoryPercentageWidth',
				colspan:1,width:me.defaultColorWidth
			},{
				fieldLabel:'显示位置',name:'HistoryPercentagePosition',
				xtype:'numberfield',itemId:'HistoryPercentagePosition',
				colspan:3,width:me.defaultPositionWidth
			},{
				boxLabel: '检验结果',name:'cbTestResult',
				xtype:'checkboxfield',itemId:'cbTestResult',
				margin:'0 5 0 5',locked:true,readOnly:true,checked:true
			}, {
				fieldLabel:'显示宽度',name:'TestResultWidth',
				xtype:'numberfield',itemId:'TestResultWidth',
				colspan:1,width:me.defaultColWidth
			},{
				fieldLabel:'显示位置',name:'TestResultPosition',
				xtype:'numberfield',itemId:'TestResultPosition',
				colspan:2,width:me.defaultPositionWidth
			}, {
				boxLabel:'异常值才有粗体显示',name:'TestResultAbNormal',
				xtype:'checkboxfield',itemId:'TestResultAbNormal',
				margin:'0 0 0 30',checked:false,width:170
			},{
				boxLabel:'仪器原始结果(淡蓝色字体)',name:'cbOriginalResult',
				xtype:'checkboxfield',itemId:'cbOriginalResult',
				margin:'0 5 0 5',checked:false
			}, {
				fieldLabel:'显示宽度',name:'OriginalResultWidth',
				xtype:'numberfield',itemId:'OriginalResultWidth',
				colspan:1,width:me.defaultColWidth
			},{
				fieldLabel:'显示位置',name:'OriginalPosition',
				xtype:'numberfield',itemId:'OriginalPosition',
				colspan:1,width:me.defaultPositionWidth,value:6
			},{
				fieldLabel:'颜色',name:'OriginalColor',
				xtype:'colorfield',itemId:'OriginalColor',allowBlank:false,
				colspan:1,width:me.defaultColorWidth,value:'#00CCFF'
			}, {
				boxLabel:'提示仪器结果更改(检验结果前红色竖条)',name:'cbOriginalChange',
				xtype:'checkboxfield',itemId:'cbOriginalChange',
				margin:'0 0 0 30',checked:false,width:250
			},{
				boxLabel:'结果单位',name:'cbResultUnit',
				xtype:'checkboxfield',itemId:'cbResultUnit',
				margin:'0 5 0 5',checked:false
			}, {
				fieldLabel:'显示宽度',name:'ResultUnitWidth',
				xtype:'numberfield',itemId:'ResultUnitWidth',
				colspan:1,width:me.defaultColWidth
			},{
				fieldLabel:'显示位置',name:'ResultUnitPosition',
				xtype:'numberfield',itemId:'ResultUnitPosition',
				value:7,colspan:3,width:me.defaultPositionWidth
			},{
				boxLabel:'参考值范围',name:'cbReference',
				xtype:'checkboxfield',itemId:'cbReference',
				margin:'0 5 0 5',checked:false
			}, {
				fieldLabel:'显示宽度',name:'ReferenceWidth',
				xtype:'numberfield',itemId:'ReferenceWidth',
				colspan:1,width:me.defaultColWidth
			},{
				fieldLabel:'显示位置',name:'ReferencePosition',
				xtype:'numberfield',itemId:'ReferencePosition',
				value:8,colspan:3,width:me.defaultPositionWidth
			},{
				boxLabel:'仪器审核状态',name:'cbExamine',
				xtype:'checkboxfield',itemId:'cbExamine',
				margin:'0 5 0 5',hidden:true,checked:false
			}, {
				fieldLabel:'显示宽度',name:'ExamineWidth',
				xtype:'numberfield',itemId:'ExamineWidth',
				colspan:1,width:me.defaultColWidth,hidden:true
			},{
				fieldLabel:'显示位置',name:'ExaminePosition',
				xtype:'numberfield',itemId:'ExaminePosition',
				value:9,colspan:2,width:me.defaultPositionWidth,hidden:true
			},{
				boxLabel:'箭头显示结果状态',name:'ArrowResult',
				xtype:'checkboxfield',itemId:'ArrowResult',
				margin:'0 0 0 5',checked: false
			},{
				boxLabel:'字符结果状态',name:'CharResult',
				xtype:'checkboxfield',itemId:'CharResult',
				margin:'0 0 0 5',locked:true,readOnly:true,checked:true
			},{
				xtype:'radiogroup',fieldLabel:'',
				columns:2,vertical:true,name:'cbResultStatus',
				colspan:3,width:240,itemId:'cbResultStatus',
				style:{marginLeft: '-5px'},
				items:[{
					boxLabel:'结果状态字体',name:'ResultStatus',inputValue:'1',checked:true
				},{
					boxLabel:'结果状态背景色',name:'ResultStatus',inputValue:'2'}
                ]
			},{
		        text:'状态颜色提示:',xtype:'label'
		     },{
				boxLabel:'项目名称颜色显示',name:'ItemNameStyle',
			    xtype:'checkboxfield',itemId:'ItemNameStyle',
				margin:'0 5 0 5',checked:false
			}, {
				boxLabel:'检验结果颜色显示',name:'ResultColor',
				xtype:'checkboxfield',itemId: 'ResultColor',
				margin:'0 5 0 5',colspan:2,width:defaults.width*1,checked:true
			},{
				boxLabel:'组合项目子项提示(项目名称黑色小方框)',name:'ExamineMsg',
				xtype:'checkboxfield',itemId:'ExamineMsg',
				margin:'0 0 0 30',width:250,checked:false
			}]
		};
		
		return fieldset;
	},
	//保存方式
	createSaveFieldSet:function(){
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;

		var fieldset = {
			xtype:'fieldset',title:'保存方式',collapsible:true,
			defaultType:'textfield',itemId:'SaveFieldSet',
			layout:layout,defaults:defaults,
			items:[{
				xtype:'radiogroup',fieldLabel:'',
				columns:3,vertical:true,colspan:4,
				itemId:'rbSaveMode',name:'rbSaveMode',
				width:defaults.width * 4+100,
				items:[{
					boxLabel:'按站点--采用相同的显示方式',name:'rbSave',inputValue:'1',checked:true
				},{
					boxLabel:'按小组--不同的小组采用不同的显示方式',name:'rbSave',inputValue:'2'
				}]
			}]
		};
		
		return fieldset;
	},
	
	//创建功能按钮栏
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		
		items.push({
			text:'采用默认设置',tooltip:'采用默认设置',
			iconCls:'button-accept',
			handler:function(){
				me.defaultValue();
			}
		},{
			text:'采用并保存',tooltip:'采用并保存',
			iconCls:'button-save',
			handler:function(){
				me.onSaveClick();
			}
		},{
			text:'删除当前小组设置',tooltip:'删除当前小组设置',
			iconCls:'button-del',
			handler:function(){
				me.onDelClick();
			}
		},{
			text:'关闭',tooltip:'关闭',
			iconCls:'button-del',
			handler:function(){
				me.close();
			}
		})
		
		if(items.length == 0) return null;
		
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:hidden
		});
	},
	//采用并保存
	onSaveClick:function(){
		var me = this,
			entity = me.getAddParams();
			
		
		
	},
	
	//获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CharResult:true,//字符结果状态
			ItemName: values.ItemName,//项目名称
			ItemNamePosition:values.ItemNamePosition,//项目位置
			ItemNameWidth:values.ItemNameWidth//项目宽度
		};
		//方块方式,填充方式
		entity.FillMode = values.cbRightFill ? '1' : '0';//自动向右填充
		
	    if(entity.FillMode=='0'){
//			entity.ColShow = values.cbNumColShow? '1' : '0';//定列显示
			entity.NumCol = values.numCol;//列数
			entity.BlockColFill = values.BlockColFill;//填充方式
		}
		entity.ContrastDate = values.cbContrastDate? '1' : '0';//历史对比日期
		entity.ContrastDateWidth = values.ContrastDateWidth;//历史对比日期显示宽度
		entity.ContrastDatePosition= values.ContrastDatePosition;//历史对比日期显示位置
		
		//序号
		entity.No = values.cbNo? '1' : '0';//序号
		entity.NoWidth = values.NoWidth;//序号显示宽度
		entity.NoPosition= values.NoPosition;//序号显示位置
		entity.NoContent= values.NoContent;//序号扩起来的符号
		
		//历史检验结果
		entity.HistoryResult = values.cbHistoryResult? '1' : '0';//历史检验结果
		entity.HistoryResultWidth = values.HistoryResultWidth;//历史检验结果显示宽度
		entity.HistoryResultPosition= values.HistoryResultPosition;//历史检验结果显示位置
		entity.HistoryResultColor= values.HistoryResultColor;//历史检验结果颜色
		//历史对比百分比
		entity.HistoryPercentage = values.cbHistoryPercentage? '1' : '0';//历史对比百分比
		entity.HistoryPercentageWidth = values.HistoryPercentageWidth;//历史对比百分比显示宽度
		entity.HistoryPercentagePosition= values.HistoryPercentagePosition;//历史对比百分比显示位置
		//检验结果
		entity.TestResult = values.cbTestResult? '1' : '0';//检验结果
		entity.TestResultWidth = values.TestResultWidth;//检验结果显示宽度
		entity.TestResultPosition = values.TestResultPosition;//检验结果显示位置
		entity.TestResultAbNormal  = values.TestResultAbNormal? '1' : '0';//异常值才有粗体显示
		//仪器原始结果(淡蓝色字体)
		entity.OriginalResult = values.cbOriginalResult? '1' : '0';//仪器原始结果
		entity.OriginalResultWidth = values.OriginalResultWidth;//仪器原始结果显示宽度
		entity.OriginalResultPosition= values.OriginalPosition;//仪器原始结果显示位置
		entity.OriginalResultColor= values.OriginalColor;//仪器原始结果颜色
		entity.OriginalChange= values.cbOriginalChange? '1' : '0';//提示仪器结果更改
		//结果单位
		entity.ResultUnit = values.cbResultUnit? '1' : '0';//结果单位
		entity.ResultUnitWidth = values.ResultUnitWidth;//结果单位显示宽度
		entity.ResultUnitPosition= values.ResultUnitPosition;//结果单位显示位置
		//参考值范围
		entity.Reference = values.cbReference? '1' : '0';//参考值范围
		entity.ReferenceWidth = values.ReferenceWidth;//参考值范围显示宽度
		entity.ReferencePosition= values.ReferencePosition;//参考值范围显示位置
		//仪器审核状态
		entity.Examine = values.cbExamine? '1' : '0';//仪器审核状态
		entity.ExamineWidth = values.ExamineWidth;//仪器审核状态显示宽度
		entity.ExaminePosition= values.ExaminePosition;//仪器审核状态显示位置
		entity.ExamineMsg= values.ExamineMsg? '1' : '0';//?组合项目子项提示
		//箭头显示结果状态
		entity.ArrowResult = values.ArrowResult? '1' : '0';//箭头显示结果状态
		//结果状态
		entity.ResultStatus= values.ResultStatus;//结果状态
		//项目名称颜色显示
		entity.ItemNameStyle= values.ItemNameStyle ? '1' : '0';//项目名称颜色显示
		//检验结果颜色显示
		entity.ResultColor= values.ResultColor ? '1' : '0';
		//保存方式
		entity.rbSaveMode= values.rbSaveMode? '1' : '2';
		
		return entity;
	},
	//获取修改的数据
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
			
		var fields = ['Id','IsProxy','ClientID','ClientName'];
		entity.fields = fields.join(',');
		if(values.PCustomerService_Id != ''){
			entity.entity.Id = values.PCustomerService_Id;
		}
		return entity;
	}
});