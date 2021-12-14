/**
 * 合同附件列表
 * @author Apollo
 * @version 2016-08-31
 */
Ext.define('Shell.class.contract.Upload', {
    extend: 'Shell.ux.form.uploadPanel.UploadContainer',
    title: '合同附件',

    width: 600,
    height: 400,

    selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachmentByHQL?isPlanish=true',
    delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectAttachmentByField',
    //新增附件文件时保存服务
    addUrl: '/ProjectProgressMonitorManageService.svc/WM_UploadNewFiles',

    //新增文件所保存的数据对象名称
    objectEName: "PProjectAttachment",
    //外键字段(如:任务表--'PTask',工作任务日志表:'PWorkTaskLog',抄送关系表:'PTaskCopyFor')
    fkObjectName: 'PTask',

    /**默认加载数据*/
    defaultLoad: true,

    /**任务ID*/
    TaskId: null,

    initComponent: function () {
        var me = this;
        me.fkObjectId = me.TaskId;
        me.callParent(arguments);
    }
});