/**
 * 合同新增表单
 * @author Apollo
 * @version 2016-08-30
 */
Ext.define('Shell.class.contract.Form', {
    extend: 'Shell.ux.form.Panel',
    requires: [
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],

    title: '合同新增表单',
    width: 235,
    height: 400,
    bodyPadding: 10,

    /**新增服务地址*/
    addUrl: '/WeiXinAppService.svc/ST_UDTO_AddPContract',
    /**修改服务地址*/
    editUrl: '/WeiXinAppService.svc/ST_UDTO_UpdatePContractByField',

    /**是否启用保存按钮*/
    hasSave: true,
    /**是否重置按钮*/
    hasReset: true,
    /*本公司名称*/
    Compname: 'OurCorName',
    /**布局方式*/
    layout: {
        type: 'table',
        columns: 4//每行有几列
    },
    /**每个组件的默认属性*/
    defaults: {
        labelWidth: 70,
        width: 196,
        labelAlign: 'right'
    },

    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        //软件价格监听
        var PContract_Software = me.getComponent('PContract_Software');
        PContract_Software.on({
            change: function () {
                me.AmountCount();
            }
        });
        //硬件价格监听
        var PContract_Hardware = me.getComponent('PContract_Hardware');
        PContract_Hardware.on({
            change: function () {
                me.AmountCount();
            }
        });
        //客户选择监听
        var PContract_PClientName = me.getComponent('PContract_PClientName'),
			PContract_PClientID = me.getComponent('PContract_PClientID');
        if (PContract_PClientName) {
            PContract_PClientName.on({
                check: function (p, record) {
                    PContract_PClientName.setValue(record ? record.get('PClient_Name') : '');
                    PContract_PClientID.setValue(record ? record.get('PClient_Id') : '');
                    p.close();
                }
            });
        }
        //付款单位选择监听
        var PContract_PayType = me.getComponent('PContract_PayType'),
                   PContract_PayTypeID = me.getComponent('PContract_PayTypeID');
        if (PContract_PayType) {
            PContract_PayType.on({
                check: function (p, record) {
                    PContract_PayType.setValue(record ? record.get('PClient_Name') : '');
                    PContract_PayTypeID.setValue(record ? record.get('PClient_Id') : '');
                    p.close();
                }
            });
        }
        //销售负责人选择监听
        var PContract_Principal = me.getComponent('PContract_Principal');
        var PContract_PrincipalID = me.getComponent('PContract_PrincipalID');
        if (PContract_Principal) {
            PContract_Principal.on({
                check: function (p, record) {
                    PContract_Principal.setValue(record ? record.get('HREmployee_CName') : '');
                    PContract_PrincipalID.setValue(record ? record.get('HREmployee_Id') : '');
                    p.close();
                }
            });
        }
        //签署人选择监听
        var PContract_SignMan = me.getComponent('PContract_SignMan');
        var PContract_SignManID = me.getComponent('PContract_SignManID');
        if (PContract_SignMan) {
            PContract_SignMan.on({
                check: function (p, record) {
                    PContract_SignMan.setValue(record ? record.get('HREmployee_CName') : '');
                    PContract_SignManID.setValue(record ? record.get('HREmployee_Id') : '');
                    p.close();
                }
            });
        }
        //本公司名称选择监听
        var PContract_Compname = me.getComponent('PContract_Compname'),
			PContract_CompnameID = me.getComponent('PContract_CompnameID');
        if (PContract_Compname) {
            PContract_Compname.on({
                check: function (p, record) {
                    PContract_Compname.setValue(record ? record.get('PDict_CName') : '');
                    PContract_CompnameID.setValue(record ? record.get('PDict_Id') : '');
                    p.close();
                }
            });
        }
        //var SServiceClientlevel_Name = me.getComponent('SServiceClientlevel_Name');
        //SServiceClientlevel_Name.on({
        //    change: function (field, newValue, oldValue, eOpts) {
        //        if (newValue != "") {
        //            JShell.Action.delay(function () {
        //                JShell.System.getPinYinZiTou(newValue, function (value) {
        //                    me.getForm().setValues({
        //                        SServiceClientlevel_PinYinZiTou: value,
        //                        SServiceClientlevel_Shortcode: value
        //                    });
        //                });
        //            }, null, 200);
        //        } else {
        //            me.getForm().setValues({
        //                SServiceClientlevel_PinYinZiTou: "",
        //                SServiceClientlevel_Shortcode: ""
        //            });
        //        }
        //    }
        //});
    },

    /**@overwrite 创建内部组件*/
    createItems: function () {
        var me = this;

        var items = [
			{
			    fieldLabel: '合同编号', name: 'PContract_ContractNumber', itemId: 'PContract_ContractNumber',
			    emptyText: '必填项', allowBlank: false, colspan: 1
			},
            {
                fieldLabel: '合同名称', name: 'PContract_Name', itemId: 'PContract_Name',
                emptyText: '必填项', allowBlank: false, width: 450, colspan:3
            },
            {
                fieldLabel: '客户选择', name: 'PContract_PClientName', itemId: 'PContract_PClientName',
                emptyText: '必填项', allowBlank: false, xtype: 'uxCheckTrigger', className: 'Shell.class.wfm.client.CheckGrid', width: 395, colspan: 2
            },
            {
                fieldLabel: '付款单位', name: 'PContract_PayType', itemId: 'PContract_PayType',
                emptyText: '必填项', allowBlank: false, xtype: 'uxCheckTrigger', className: 'Shell.class.wfm.client.CheckGrid', width: 360, colspan: 2
            },
            {
                fieldLabel: '有偿服务应开始时间', name: 'PContract_PaidServiceStartTime', itemId: 'PContract_PaidServiceStartTime',
                emptyText: '必填项', xtype: 'datefield', format: 'Y-m-d', colspan: 1
            },
            {
                fieldLabel: '销售负责人', name: 'PContract_Principal', itemId: 'PContract_Principal',
                emptyText: '必填项', allowBlank: false, itemId: 'PContract_Principal', xtype: 'uxCheckTrigger', className: 'Shell.class.sysbase.user.CheckApp', colspan: 1
            },            
			{
			    fieldLabel: '签署日期', name: 'PContract_SignDate', itemId: 'PContract_SignDate',
			    emptyText: '必填项', allowBlank: false, xtype: 'datefield', format: 'Y-m-d', colspan: 1
			},
            {
                fieldLabel: '签署人', name: 'PContract_SignMan', itemId: 'PContract_SignMan',
                emptyText: '必填项', allowBlank: false, xtype: 'uxCheckTrigger', className: 'Shell.class.sysbase.user.CheckApp', width: 160, colspan: 1
            },
            {
                fieldLabel: '软件价格', name: 'PContract_Software', itemId: 'PContract_Software',
                value: 0, emptyText: '必填项', xtype: 'numberfield'
            },
            {
                fieldLabel: '硬件价格', name: 'PContract_Hardware', itemId: 'PContract_Hardware',
                value: 0, emptyText: '必填项', xtype: 'numberfield'
            },
            {
                fieldLabel: '合同总额', name: 'PContract_Amount', itemId: 'PContract_Amount',
                value: 0, xtype: 'numberfield'
            },
            {
                fieldLabel: '其它费用', name: 'PContract_MiddleFee', itemId: 'PContract_MiddleFee',
                width: 160, value: 0, xtype: 'numberfield'
            },
            {
                fieldLabel: '本公司名称', name: 'PContract_Compname', itemId: 'PContract_Compname',
                emptyText: '必填项', allowBlank: false, xtype: 'uxCheckTrigger', className: 'Shell.class.wfm.dict.CheckGrid', width: 395, colspan: 2,
                classConfig: {
                    title: '本公司名称选择',
                    defaultWhere: "pdict.PDictType.DictTypeCode='" + this.Compname + "'"
                }
            },
			{
			    boxLabel: '是否开具发表', name: 'PContract_IsInvoices', itemId: 'PContract_IsInvoices', colspan: 2,
			    xtype: 'checkbox', checked: true, style: { marginLeft: '75px' }
			},
			
            {
                fieldLabel: '备注', name: 'PContract_Comment', itemId: 'PContract_Comment',//resizable:true,resizeHandles:'s',
                minHeight: 100, style: { marginBottom: '10px' }, width: 450,colspan: 4, xtype: 'textarea'//xtype:'htmleditor'
            },
			
			
			{ fieldLabel: '主键ID', name: 'PContract_Id', itemId: 'PContract_Id', hidden: true },
			{ fieldLabel: '客户主键ID', name: 'PContract_PClientID', itemId: 'PContract_PClientID', hidden: true },
            { fieldLabel: '付款单位ID', name: 'PContract_PayTypeID', itemId: 'PContract_PayTypeID', hidden: true },
            { fieldLabel: '销售负责人主键ID', hidden: true, name: 'PContract_PrincipalID', itemId: 'PContract_PrincipalID' },
            { fieldLabel: '所属部门ID', hidden: true, name: 'PContract_DeptID', itemId: 'PContract_DeptID' },
            { fieldLabel: '签署人ID', hidden: true, name: 'PContract_SignManID', itemId: 'PContract_SignManID' },
            { fieldLabel: '本公司ID', hidden: true, name: 'PContract_CompnameID', itemId: 'PContract_CompnameID' }
        ];

        return items;
    },
    /**@overwrite 获取新增的数据*/
    getAddParams: function () {
        var me = this,
			values = me.getForm().getValues();

        var entity = {
            ContractNumber: values.PContract_ContractNumber,
            SName: values.PContract_Name,
            Code: values.SServiceClientlevel_Code,
            PinYinZiTou: values.SServiceClientlevel_PinYinZiTou,
            Shortcode: values.SServiceClientlevel_Shortcode,
            DispOrder: values.SServiceClientlevel_DispOrder,
            IsUse: values.SServiceClientlevel_IsUse ? true : false,
            Comment: values.SServiceClientlevel_Comment
        };

        return { entity: entity };
    },
    /**@overwrite 获取修改的数据*/
    getEditParams: function () {
        var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

        for (var i in fields) {
            var arr = fields[i].split('_');
            if (arr.length > 2) continue;
            fieldsArr.push(arr[1]);
        }
        entity.fields = fieldsArr.join(',');

        entity.entity.Id = values.SServiceClientlevel_Id;
        return entity;
    },
    /**更改标题*/
    changeTitle: function () {
        //不做处理
    },
    /**@overwrite 返回数据处理方法*/
    changeResult: function (data) {
        return data;
    },
    /**保存按钮点击处理方法*/
    onSaveClick: function () {
        alert(0);
        var me = this;
        me.callParent(arguments);        
    },
    AmountCount: function ()
    {
        var me = this;
        var PContract_Amount = me.getComponent('PContract_Amount');
        //alert(me.getComponent('PContract_Hardware').getValue());
        PContract_Amount.setValue(me.getComponent('PContract_Software').getValue() + me.getComponent('PContract_Hardware').getValue());
    }
});