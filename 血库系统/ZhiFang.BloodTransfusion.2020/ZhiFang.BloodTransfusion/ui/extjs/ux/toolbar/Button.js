/**
 * 功能按钮栏
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.toolbar.Button',{
    extend:'Ext.toolbar.Toolbar',
    alias:'widget.uxButtontoolbar',
    mixins:['Shell.ux.Langage'],
    
    /**提示框样式*/
    tooltipFormat:'<b>{msg}</b>',
    /**按钮方法规则*/
    handlerFormat:'on{fun}Click',
    /**预定义按钮信息*/
    Shell_ux_toolbar_Button:{
    	REFRESH:{text:'刷新',tooltip:'刷新',iconCls:'button-refresh',funName:'Refresh'},
    	ADD:{text:'新增',tooltip:'新增',iconCls:'button-add',funName:'Add'},
    	EDIT:{text:'编辑',tooltip:'编辑',iconCls:'button-edit',funName:'Edit'},
    	DEL:{text:'删除',tooltip:'删除',iconCls:'button-del',funName:'Del'},
    	SHOW:{text:'查看',tooltip:'查看',iconCls:'button-show',funName:'Show'},
    	SAVE:{text:'保存',tooltip:'保存',iconCls:'button-save',funName:'Save'},
    	SAVEAS:{text:'另存',tooltip:'另存',iconCls:'button-saveas',funName:'Saveas'},
    	RESET:{text:'重置',tooltip:'重置',iconCls:'button-reset',funName:'Reset'},
    	CANCEL:{text:'取消',tooltip:'取消',iconCls:'button-cancel',funName:'Cancel'},
    	ACCEPT:{text:'确定',tooltip:'确定',iconCls:'button-accept',funName:'Accept'},
    	COPY:{text:'复制',tooltip:'复制',iconCls:'button-edit',funName:'Copy'},
    	IMPORT:{text:'导入',tooltip:'导入',iconCls:'button-import',funName:'Import'},
    	EXP:{text:'导出',tooltip:'导出',iconCls:'button-exp',funName:'Exp'},
    	IMPORT_EXCEL:{text:'导入',tooltip:'EXCEL导入',iconCls:'file-excel',funName:'ImportExcel'},
    	EXP_EXCEL:{text:'导出',tooltip:'EXCEL导出',iconCls:'file-excel',funName:'ExpExcel'},
    	LOCK:{text:'锁定',tooltip:'锁定',iconCls:'button-lock',funName:'Lock'},
    	LOCKOPEN:{text:'解锁',tooltip:'解除锁定',iconCls:'button-lock-open',funName:'LockOpen'},
    	LOGIN:{text:'登录',tooltip:'登录系统',iconCls:'button-login',funName:'Login'},
    	SEARCHB:{text:'查询',tooltip:'查询',iconCls:'button-search',funName:'SearchB'},
    	COLLAPSE_RIGHT:{tooltip:'收缩面板',iconCls:'button-right',funName:'Collapse'},
    	COLLAPSE_DOWN:{tooltip:'收缩面板',iconCls:'button-down',funName:'Collapse'},
    	COLLAPSE_LEFT:{tooltip:'收缩面板',iconCls:'button-left',funName:'Collapse'},
    	COLLAPSE_UP:{tooltip:'收缩面板',iconCls:'button-up',funName:'Collapse'},
    	CHECK:{text:'审核',tooltip:'审核',iconCls:'button-check',funName:'Check'},
    	UNCHECK:{text:'取消审核',tooltip:'取消审核',iconCls:'button-uncheck',funName:'Uncheck'},
    	PRINT:{text:'打印',tooltip:'打印',iconCls:'button-print',funName:'Print'},
    	CONFIG:{text:'设置',tooltip:'设置',iconCls:'button-config',funName:'Config'}
    },
	
	initComponent:function(){
		var me = this;
		
		//替换语言包
		me.changeLangage('Shell.ux.toolbar.Button');
		
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部*/
	createItems:function(){
		var me = this,
			items = me.items || [],
			len = items.length;
			
		for(var i=0;i<len;i++){
			if(typeof(items[i]) === 'string'){
				items[i] = me.createPredefinedButton(items[i]);
			}else{
				if(items[i].type && items[i].type.toLocaleUpperCase() == 'SEARCH'){
					items[i] = me.createSearch(items[i]);
				}
			}
		}
		
		return items;
	},
	/**创建预定义功能*/
	createPredefinedButton:function(item){
		if(!item) return null;
		var me = this;
		var info = me.Shell_ux_toolbar_Button[item.toLocaleUpperCase()];
		
		if(!info) return item;
		
		return {
			xtype:'button',
			text:info.text,
			itemId:'' + item,
			tooltip:me.tooltipFormat.replace(/{msg}/,info.tooltip),
			iconCls:info.iconCls,
			handler:function(){
				var funName = me.handlerFormat.replace(/{fun}/,info.funName);
				if(me.ownerCt[funName]){
					me.ownerCt[funName](me.ownerCt);
				}
			}
		};
	},
	/**创建查询框*/
	createSearch:function(item){
		var me = this;
		
		var search = {
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			onTriggerClick:function(){
				if(me.ownerCt['onSearchClick']){
					me.ownerCt['onSearchClick'](me,this.getValue());
				}
			},
			listeners:{
	            keyup:{
	                fn:function(field,e){
	                	if(e.getKey() == Ext.EventObject.ESC){
	                		field.setValue('');
	                		if(me.ownerCt['onSearchClick']){
								me.ownerCt['onSearchClick'](me,field.getValue());
							}
	                	}else if(e.getKey() == Ext.EventObject.ENTER){
							if(me.ownerCt['onSearchClick']){
								me.ownerCt['onSearchClick'](me,field.getValue());
							}
	                	}
	                }
	            }
	        }
		};
		
		return Ext.apply(search,item.info);
	}
});