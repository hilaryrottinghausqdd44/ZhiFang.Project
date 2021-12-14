/**
 * 账户密码维护
 * @author jing
 * @version 2018-09-17
 */
Ext.define('Shell.class.siteQuery.userSign.EditPwd', {
    extend: 'Ext.form.Panel',
    title: '账户密码维护',
    width: 280,
    height: 160,
    user:'',
    /**内容周围距离*/
    bodyPadding: 10,
    /**布局方式*/
    layout: 'anchor',
    /** 每个组件的默认属性*/
    defaults: {
        anchor: '100%',
        labelWidth: 70,
        labelAlign: 'right'
    },

    afterRender: function () {
        var me = this;
        me.formtype = 'edit';
        me.callParent(arguments);
    },
    initComponent: function () {
        var me = this;

        me.items = me.createItems();
        me.dockedItems = me.createDocked();
        me.callParent(arguments);
    },
    createDocked:function () {
        var me = this;
        return [Ext.create('Ext.toolbar.Toolbar', {
            dock: 'bottom',
            items: ['->', {
                xtype: 'button',
                iconCls: 'button-save',
                text: '保存',
                handler: function () {
                    me.savePwd();
                }
            }, {
                xtype: 'button',
                iconCls: 'button-cancel',
                text: '取消',
                handler: function () {
                    me.close();
                }
            }
            ]
        })]
    },
    savePwd:function () {
        var me = this,
			values = me.getForm().getValues();

        //判断两次新密码输入是否一致
        if (values.newPwd1 != values.newPwd2) {
            Shell.util.Msg.showError('请确认新密码正确!');
            return false;
        }
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath+"/ServiceWCF/DictionaryService.svc/UpdateUserPwd",
            async: false,
            method: 'post',
            params: Ext.encode({
                "userNo":me.user,
                "oldPwd": values.pwd,
                "newPwd": values.newPwd1
            }),
            success: function (reponse) {
                rs = Ext.JSON.decode(reponse.responseText);
                if (rs.success) {
                    Shell.util.Msg.showInfo("修改成功");
                    me.close();
                } else {
                    Shell.util.Msg.showError(rs.ErrorInfo);
                }
            }
        });
    },
    /**创建内部组件*/
    createItems: function () {
        var me = this;
        var items = [{
            xtype:'textfield',
            fieldLabel: '当前密码',
            emptyText: '当前密码',
            allowBlank: false,
            itemId: 'pwd',
            name: 'pwd',
            inputType: 'password'
        }, {
            fieldLabel: '新密码',
            emptyText: '新密码',
            allowBlank: false,
            xtype: 'textfield',
            itemId: 'newPwd1',
            name: 'newPwd1',
            inputType: 'password'
        }, {
            fieldLabel: '确认新密码',
            emptyText: '确认新密码',
            allowBlank: false,
            xtype: 'textfield',
            itemId: 'newPwd2',
            name: 'newPwd2',
            inputType: 'password'
        }];

        return items;
    }
   
});