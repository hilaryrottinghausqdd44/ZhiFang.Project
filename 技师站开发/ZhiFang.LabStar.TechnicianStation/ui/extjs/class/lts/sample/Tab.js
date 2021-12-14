/**
 * 样本检验TAB
 * @author Jcall
 * @version 2019-10-18
 */
Ext.define('Shell.class.lts.sample.Tab', {
	extend:'Ext.tab.Panel',
	title:'样本检验',
	//全部页签ItemId
	ALL_ITEMID:'group_all',
	//添加按钮ItemId
	ADD_ITEMID:'button_add',
	
	defaults:{border:false},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			remove:function(con,com){
				//操作存储中添加小组页签
				me.OpenedSection.remove(com.itemId);
			}
		});
		setTimeout(function(){
			//打开最后一次记录的Tab页签
			me.onOpenLastTab();
		},500);
	},
    
	initComponent:function(){
		var me = this;
		me.activeTab = 0;//初始页签
		
		//操作数据-打开的小组
		me.OpenedSection = Ext.create('Shell.class.basic.data.OpenedSection');
		
		me.items = [Ext.create('Shell.class.lts.sample.App',{
			border:false,closable:false,
			itemId:me.ALL_ITEMID,
			sectionId:null,
			title:'全部',
			isReadOnly: true,
			listeners: {
				AllGroupOnBeforeLoad: function (p) {//根据小组页签初始化全部页签中列表的默认查询条件 当前存在页签的小组
					var sectionIds = [];
					for (var i = 0; i < me.items.items.length; i++) {
						if (me.items.items[i].sectionId) sectionIds.push(me.items.items[i].sectionId);
					}
					if (sectionIds.length > 0)
						p.defaultWhere = 'listestform.LBSection.Id in (' + sectionIds.join(",") + ')';
					else
						p.defaultWhere = 'listestform.LBSection.Id = null';
				}
			}
		})];
		
		me.tabBar = {
			items:[{
				text:'+',tooltip:'添加小组',closable:false,
				itemId:me.ADD_ITEMID,handler:function(but,e){me.onAddGroup(e);}
			}]
		};
		
		me.callParent(arguments);
	},
	//打开最后一次记录的Tab页签
	onOpenLastTab:function(){
		var me = this,
			sectionList = me.OpenedSection.getList();
		
		for(var i in sectionList){
			me.onAddTab(sectionList[i]);
		}
		//小组页签全部打开后 再执行全部页签中的列表查询
		me.getComponent(me.ALL_ITEMID).Grid.onSearch();
	},
	onAddGroup:function(e){
		var me = this,
			list = me.items.items,
			checkedIds = [];
			
		for(var i in list){
			if(list[i].itemId == me.ALL_ITEMID) continue;
			checkedIds.push(list[i].itemId);
		}
		
		JShell.Win.open('Shell.class.lts.section.role.CheckGrid',{
			checkedIds:checkedIds.join(','),
			listeners:{
				accept:function(win,records){
					me.onSectionChecked(records);
					win.close();
				}
			}
		}).showAt(e.getXY());
	},
	//小组选中后处理
	onSectionChecked:function(records){
		var me = this;
		
		for(var i in records){
			me.onAddSectionTab({
				Id:records[i].get('LBRight_LBSection_Id'),
				Name:records[i].get('LBRight_LBSection_CName')
			});
		}
	},
	//新增一个小组页签
	onAddSectionTab:function(info){
		var me = this;
			
		//操作存储中添加小组页签
		me.OpenedSection.set(info);
		//添加页签
		me.onAddTab(info);
	},
	//添加页签
	onAddTab:function(info){
		var me = this,
			count = me.items.length;
			
		var tab = me.getComponent(info.id) || me.insert(count,Ext.create('Shell.class.lts.sample.App',{
			closable:true,
			border:false,
			itemId:info.Id,
			sectionId:info.Id,
			title: info.Name,
			listeners: {
				close: function (p) {
					p.removeKeyDown(p);
					//处理关闭TabPanel时出现激活页签是关闭的页签情况
					if (me.activeTab.itemId == p.itemId) {//如果当前激活页签是关闭的页签时
						var flag = false,
							Tabs = me.items.items,
							lastTab = null;
						for (var i = 0; i < Tabs.length; i++) {
							if (p.itemId == Tabs[i].itemId) continue;
							if (Tabs[i].itemId == me.activeTab.itemId) {
								flag = true;
								break;
							}
							lastTab = Tabs[i];
						}
						if (!flag) me.setActiveTab(lastTab);
					}
				},
				show: function (t) {
					tab.onKeyDown(t);
				},
				hide: function (t) {
					tab.removeKeyDown(t);
				}
			}
		}));
		me.setActiveTab(tab);
	}
});