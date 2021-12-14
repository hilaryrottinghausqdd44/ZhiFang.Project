Ext.define("Shell.class.setting.ScriptingOptions.update.scriptGrid", {
    extend: 'Shell.ux.panel.Grid',
    requires: [
        'Ext.ux.CheckColumn',
        'Shell.ux.form.field.CheckTrigger'
    ],
    /**获取数据服务路径*/
    selectUrl: '',
    /**是否启用修改按钮*/
    hasEdit: false,
   /**是否启用新增按钮*/
	hasAdd:true,
    /**是否启用保存按钮*/
    hasSave: false,
    copyTims: 0,
    /**默认加载*/
    defaultLoad: false,

    /**复制按钮点击次数*/
    copyTims: 0,


    /**查询栏参数设置*/
    searchToolbarConfig: {},

    initComponent: function () {
        var me = this;
        me.columns = me.createGcolumns();
        me.callParent(arguments);
    },

    afterRender: function () {
        var me = this;
        me.callParent(arguments);
    },

    createGcolumns: function () {
        var me = this;
        var columns = [
            {
			text:'版本号',
            dataIndex:'Id',
			width:60,
			isKey:true,
			},{
				text:'升级内容简述',
	            dataIndex:'Describe',
				width:800,
			}];
        return columns;
    },

    /**创建挂靠功能栏*/
    createDockedItems: function () {
        var me = this,
            items = me.dockedItems || [];
        if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
        if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

        return items;
    },/**@overwrite 新增按钮点击处理方法*/
    onAddClick: function () {
        var me = this;
        if (me.isadd) {
            JShell.Win.open("Shell.class.weblis.lc.lc_goods.ADDForm", {
                width: 500,
                height: 300,
                BLaboratoryArea_AreaCName: me.BLaboratoryArea_AreaCName,
                BLaboratoryArea_Id: me.BLaboratoryArea_Id,
                listeners: {
                    save: function (m, rs) {
                        if (rs) {
                            m.close();
                            me.onSearch();
                            JShell.Msg.alert("添加成功！");
                        } else {
                            JShell.Msg.error(rs.ErrorInfo);
                        }
                    }
                }
            }).show();
            this.fireEvent('addclick');
        } else {
            JShell.Msg.error("请点击左侧的区域在进行添加");
        }
        
    }
	
	
});
