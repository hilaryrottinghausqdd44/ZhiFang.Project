/**
 * 历史对比-查询条件
 * @author Jcall
 * @version 2020-03-27
 * @author guohx	
 * @version 2020-04-17
 */
Ext.define('Shell.class.lts.sample.result.history.SearchForm', {
	extend:'Shell.ux.form.Panel',
	title: '查询条件',
	layout: 'anchor',
	//查询历史对比小组url
	queryLBSectionHisCompUrl:JShell.System.Path.ROOT+'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionHisCompByHQL?isPlanish=true',
	queryLBSectionurl:JShell.System.Path.ROOT+'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',
	//每个组件的默认属性
	defaults: {
		anchor: '100%'
	},
	formtype: 'edit',
	bodyPadding: 5,
	sectionId:'',
	testFormRecord:'',
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function () {
		var me = this;
		//me.html = me.title;//正式功能需要注释
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//@overwrite 创建内部组件
	createItems: function () {
		var me = this;

		var items = [
			{
				xtype: 'fieldset',
				title: '历史对比范围',
				itemId: 'historyComparisonScope',
				collapsible: true,
				defaultType: 'textfield',
				layout: {
					type: 'table',
					columns: 2,
					tableAttrs: {
						cellpadding: 0,
						cellspacing: 0,
						width: '100%',
						align: 'right'

					}
				},
				defaults: {
					anchor: '100%',
					labelWidth: 60,
					labelAlign: 'right'
				},
				items: me.createHistoryComparisonScopeItems()
			}, {
				xtype: 'fieldset',
				title: '对比小组范围选择',
				itemId: 'sectionScope',
				collapsible: true,
				defaultType: 'textfield',
				layout: 'anchor',
				defaults: {
					anchor: '100%',
					labelWidth: 60,
					labelAlign: 'right'
				}
			},{
	            xtype: 'button',
	            text : '确认对比范围',
	            listeners: {
			        click: function() {
			            me.queryTestform();
			        }
			    }
	        }
		];
		return items;
	},
	//创建历史对比范围组件
	createHistoryComparisonScopeItems: function () {
		var me = this;
		var items = [
			{
				xtype: 'checkbox',
				name: 'IsUseLisTestForm_GSampleTypeID',
				itemId: 'IsUseLisTestForm_GSampleTypeID'
			}, {
				fieldLabel: '样本类型', xtype: 'uxCheckTrigger',
				name: 'LisTestForm_LBSampleType',
				itemId:'LisTestForm_LBSampleType',
				className: 'Shell.class.basic.sampleType.SampleType',
				width: 190,
				listeners: {
					check: function (p, record) {
						p.setValue(record ? record.get('LBSampleType_CName') : '');
						p.nextNode().setValue(record ? record.get('LBSampleType_Id') : '');
						p.close();
					}
				}
			}, {
				fieldLabel: '样本类型ID',
				name: 'LisTestForm_GSampleTypeID',
				itemId:'LisTestForm_GSampleTypeID',
				hidden: true
			}, {
				xtype: 'container',
				html: '&nbsp;'
			}, {
				fieldLabel: '开始日期', name: 'LisTestForm_GTestDate',itemId:'LisTestForm_GTestDate_start',
				xtype: 'datefield', format: 'Y-m-d', width: 190,value:JcallShell.Date.getNextDate(new Date(), 1 - 60)
			}, {
				xtype: 'container',
				html: '&nbsp;'
			}, {
				fieldLabel: '截止日期', name: 'LisTestForm_LBTestDate',itemId:'LisTestForm_GTestDate_end',
				xtype: 'datefield', format: 'Y-m-d', width: 190,value:new Date()
			}, {
				xtype: 'checkbox',
				name: 'IsUsePresentTestItem',
				itemId: 'IsUsePresentTestItem'
			}, {
				xtype: 'text',
				text: '仅显示当前检验项目',
				padding: '0 0 0 8'
			}];
		return items;
	},
	//
	createSectionScopeItems: function () {
		var me = this;		
		me.queryLBSectionHisCompUrl += "&where=LBSection.id="+me.sectionId+"&fields=LBSectionHisComp_LBSection_Id,LBSectionHisComp_LBSection_CName,LBSectionHisComp_HisComp_Id,LBSectionHisComp_HisComp_CName";
		me.queryLBSectionurl += "&where=lbsection.id!="+me.sectionId+"&fields=LBSection_Id,LBSection_CName";
		JcallShell.Server.get(me.queryLBSectionHisCompUrl,function(data){
			if(data.success && data.value.count > 0){
				var items = [];
				for(var i = 0;i<data.value.count;i++){
					var item = {
						xtype: 'checkbox',
						name: 'IsUseLisTestForm_sectionid'+data.value.list[i].LBSectionHisComp_HisComp_Id,
						itemId: 'IsUseLisTestForm_sectionid'+data.value.list[i].LBSectionHisComp_HisComp_Id,
						boxLabel: data.value.list[i].LBSectionHisComp_HisComp_CName
					};
					items.push(item);
				}
				var sectionScope = me.getComponent("sectionScope");
				sectionScope.add(items); 
			}else{
				JcallShell.Server.get(me.queryLBSectionurl,function(data){
					if(data.success && data.value.count > 0){
						var items = [];
						for(var i = 0;i<data.value.count;i++){
							var item = {
								xtype: 'checkbox',
								name: 'IsUseLisTestForm_sectionid'+data.value.list[i].LBSection_Id,
								itemId: 'IsUseLisTestForm_sectionid'+data.value.list[i].LBSection_Id,
								boxLabel: data.value.list[i].LBSection_CName
							};
							items.push(item);
						}
						var sectionScope = me.getComponent("sectionScope");
						sectionScope.add(items); 
					}
				});	
			}
		});	
	},
	queryTestform:function(){
		var me = this,
		  hcs = me.getComponent("historyComparisonScope"),
		  sectionScope = me.getComponent("sectionScope"),
		  staredate = hcs.getComponent("LisTestForm_GTestDate_start").rawValue,
		  enddate = hcs.getComponent("LisTestForm_GTestDate_end").rawValue,
		  where = " 1=1 and PatNo = " + me.testFormRecord.get("LisTestForm_PatNo");
		if(me.testFormRecord.get("LisTestForm_PatNo")=="" || me.testFormRecord.get("LisTestForm_PatNo") == null){
			JcallShell.Msg.error("病历号为空");
			return;
		}
		if(staredate == "" && enddate != ""){
			where += " and GTestDate = '"+enddate+"'";
		}else if (staredate != "" && enddate == ""){
			where += " and GTestDate = '"+staredate+"'";
		}else if(staredate != "" && enddate != ""){
			where += " and GTestDate >= '"+staredate+"' and GTestDate <= '"+enddate+"'";
		}
		if(hcs.getComponent("IsUsePresentTestItem").checked){
			where += " and SectionId = " + me.sectionId + " and ReprotFormID = "+ me.testFormRecord.get("LisTestForm_Id");
		}else{
			if(hcs.getComponent("IsUseLisTestForm_GSampleTypeID").checked){
				var gsampleTypeID= hcs.getComponent("LisTestForm_GSampleTypeID").rawValue;
				if(gsampleTypeID != "" && gsampleTypeID != null){
					where += " and GSampleTypeID = " + gsampleTypeID;
				}			
			}
			var sectionno = me.sectionId;
			for(var i=0;i<sectionScope.items.length;i++){
				if(sectionScope.getComponent(sectionScope.items.keys[i]).checked){
					sectionno +=","+sectionScope.items.keys[i].replace("IsUseLisTestForm_sectionid","");
				}
			}
			where += " and SectionId in ("+sectionno+")";
		}
		me.fireEvent('searchHisCom', where);
	}
});