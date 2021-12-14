/**
 * 合同申请列表
 * @author Jcall
 * @version 2016-11-14
 */
Ext.define('Shell.class.wfm.business.contract.manage.Grid', {
    extend: 'Shell.class.wfm.business.contract.basic.SearchGrid',

    title: '合同申请列表',
	
    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用删除按钮*/
    hasDel:true,
    /**是否启用查询框*/
    hasSearch: true,

    /**复选框*/
    multiSelect: true,
    selType: 'checkboxmodel',
	
    initComponent: function () {
        var me = this;
		
        me.callParent(arguments);
    },
    /**改变默认条件*/
	changeDefaultWhere:function(){
		this.defaultWhere = '';
	},
    /**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
			
		columns.splice(3,0,{
			xtype: 'actioncolumn',
			text: '编辑',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls:'button-edit hand',
				tooltip:'编辑',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.openEditForm(id);
				}
			}]
		});
			
		return columns;
	},
    /**修改*/
    openEditForm: function (id) {
        var me = this;
        JShell.Win.open('Shell.class.wfm.business.contract.manage.EditPanel', {
        	SUB_WIN_NO:'1',//内部窗口编号
            //resizable:false,
            title:'合同编辑',
            formtype:'edit',
            PK:id,
            listeners:{
                save:function(p,id){
                    p.close();
                    me.onSearch();
                }
            }
        }).show();
    }
});