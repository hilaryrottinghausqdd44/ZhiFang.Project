/**
 * 合同新增页面
 * @author Apollo
 * @version 2016-08-29
 */
Ext.define('Shell.class.contract.AddPanel', {
    extend: 'Ext.tab.Panel',
    title: '合同新增页面',

    width: 800,
    height: 600,

    autoSrcoll: false,
    FormConfig: null,

    /**任务ID*/
    ContractId: null,
    /**保存数据提示*/
    saveText: JShell.Server.SAVE_TEXT,

    afterRender: function () {
        var me = this;
        me.callParent(arguments);

        me.Form.on({
            beforesave: function (p) {
                me.showMask(me.saveText);
            },
            aftersave: function (p, id) {
                alert(id);
                //me.hideMask();
                //me.ContractId = id;
                //me.onSaveAttachment(id);
            }
        });
        me.Attachment.on({
            save: function (p) {
                me.hideMask();
                me.fireEvent('save', me, me.ContractId);
            }
        });
    },
    initComponent: function () {
        var me = this;
        me.addEvents('save');
        me.items = me.createItems();

        me.callParent(arguments);
    },
    /**创建内部组件*/
    createItems: function () {
        var me = this;

        me.Form = Ext.create('Shell.class.contract.Form', Ext.apply(me.FormConfig, {
            formtype: 'add',
            hasLoadMask: false,//开启加载数据遮罩层
            title: '合同内容'
        }));
        me.ContentForm = Ext.create('Shell.class.contract.ContentForm', {
            title: '附加信息',
            header: false,
            height: me.height,
            width: me.width,
            itemId: 'ContentForm',
            border: false
        });
        me.Attachment = Ext.create('Shell.class.wfm.task.attachment.Upload', {
            title: '合同附件'
        });

        return [me.Form, me.ContentForm, me.Attachment];
    },
    onSaveAttachment: function (id) {
        var me = this;

        me.showMask(me.saveText);

        me.Attachment.setValues({ fkObjectId: id });
        me.Attachment.save();
    },
    /**显示遮罩*/
    showMask: function (text) {
        var me = this;
        me.body.mask(text);//显示遮罩层
    },
    /**隐藏遮罩*/
    hideMask: function () {
        var me = this;
        if (me.body) { me.body.unmask(); }//隐藏遮罩层
    }
});