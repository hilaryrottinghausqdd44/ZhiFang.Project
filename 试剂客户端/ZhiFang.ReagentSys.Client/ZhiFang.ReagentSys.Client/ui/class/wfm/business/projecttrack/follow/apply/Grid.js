/**
 * 项目跟踪申请列表
 * @author liangyl	
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.apply.Grid', {
    extend: 'Shell.class.wfm.business.projecttrack.follow.basic.Grid',

    title: '项目跟踪申请列表',
	
    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用新增按钮*/
    hasAdd: true,
    /**是否启用查询框*/
    hasSearch: true,
	 /**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere = 'pcontractfollow.PrincipalID='+userId +' or pcontractfollow.ApplyManID='+userId;
		me.load(null, true, autoSelect);
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
				getClass: function(v, meta, record) {
					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || '-1';
					var ApplyManID = record.get('PContractFollow_ApplyManID') + '';
					return 'button-edit hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.openEditForm(id);
				}
			}]
		});
		
		columns.push({
			text:'申请人ID',dataIndex:'PContractFollow_ApplyManID',hidden:true,hideable:false
		});
		
		return columns;
	},
	
    onAddClick: function () {
        var me = this;
        JShell.Win.open('Shell.class.wfm.business.projecttrack.follow.apply.AddPanel', {
        	SUB_WIN_NO:'1',//内部窗口编号
        	//resizable:false,
            title:'项目跟踪申请',
            formtype:'add',
            listeners:{
                save:function(p,id){
                    p.close();
                    me.onSearch();
                }
            }
        }).show();
    },
    /**修改*/
    openEditForm: function (id) {
        var me = this;
        JShell.Win.open('Shell.class.wfm.business.projecttrack.follow.apply.EditPanel', {
        	SUB_WIN_NO:'2',//内部窗口编号
            //resizable:false,
            title:'项目跟踪编辑',
            formtype:'edit',
            PK:id,
            listeners:{
                save:function(p,id){
                    p.close();
                    me.onSearch();
                }
            }
        }).show();
    },
    createButtonToolbarItems:function () {
    	var me = this,
    	    buttonToolbarItems = me.callParent(arguments);
    	buttonToolbarItems.splice(1,0,'-','add');
    	return buttonToolbarItems;
    }
    
});