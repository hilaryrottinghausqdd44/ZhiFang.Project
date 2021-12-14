/**
 * 导航栏
 * @author jing
 * @version 2018-09-17
 */
Ext.define("Shell.class.siteQuery.Title", {
    extend: 'Ext.panel.Panel',
    cookie:'',
    initComponent: function () {
        var me = this;
        me.dockedItems=me.createDocked();
        me.callParent(arguments);
    },

    setTitleName:function () {
        var me = this;
        var name = "当前用户:";
        if (me.cookie != '' && me.cookie != null) {
            var ck = me.cookie;
            name += ck.CName;
        }
        return name;
    },

    createDocked: function () {
        var me = this;
       return [Ext.create('Ext.toolbar.Toolbar', {
            dock: 'top',
            items: [
            {
                xtype: "component",
                width: 20, //图片宽度     
                height: 20, //图片高度     
                autoEl: {
                    tag: 'img',    //指定为img标签     
                    src: Shell.util.Path.uiPath + '/images/login/logo-16.png'    //指定url路径     
                }
            }, {
                xtype: 'label',
                text: "报告查询系统",
                style: 'color: rgb(4, 64, 140);font-weight: bold;font-size: 16px;left: 23px;top: -1px;position: absolute;'
            }, '->', {
                xtype: 'splitbutton',
                itemId: 'UserInfo',
                textAlign: 'left',
                iconCls: 'button-user',
                hidden: false,
                text: me.setTitleName(),
                handler: function (btn, e) {
                    btn.overMenuTrigger = true;
                    btn.onClick(e);
                },
                menu: [{
                    text: '锁定账户',
                    iconCls: 'button-lock',
                    name: 'lock',
                    listeners: {
                        click: function (but) {
                            me.LockAccount();
                        }
                    }
                }, {
                    text: '切换账户',
                    iconCls: 'button-login',
                    name: 'change',
                    listeners: {
                        click: function (but) {
                            window.location.href = Shell.util.Path.uiPath + '/class/siteQuery/userSign/index.html';
                        }
                    }
                }, {
                    text: '修改密码',
                    iconCls: 'button-edit',
                    name: 'edit',
                    listeners: {
                        click: function (but) {
                            me.onEditPwd();
                        }
                    }
                }]
            }
            ]
        })]
    },

    LockAccount:function () {
        var me = this;
        Shell.util.Win.open('Shell.class.userSign.app.App', {
            formtype: 'add',
            resizable: false,
            maximizable: false,//是否带最大化功能
            closable: false,
            Account:me.cookie.UserNo,
            listeners: {
                login: function (p) {
                    p.close();
                }
            }
        }).show();
    },
    onEditPwd: function () {
        var me = this;
        Shell.util.Win.open('Shell.class.userSign.app.EditPwd', {
            resizable: false,
            maximizable: false,//是否带最大化功能
            closable: false,
            user:me.cookie.UserNo,
            listeners: {
                login: function (p) {
                    p.close();
                }
            }
        }).show();
    }
});