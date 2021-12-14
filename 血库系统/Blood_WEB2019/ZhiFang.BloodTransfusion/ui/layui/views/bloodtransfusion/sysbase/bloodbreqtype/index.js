layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	gridpanel: 'ux/gridpanel',
	bloodbreqtypeTable: '/views/bloodtransfusion/sysbase/bloodbreqtype/bloodbreqtypeTable',
	breqtypeForm: '/views/bloodtransfusion/sysbase/bloodbreqtype/breqtypeForm'
}).use(['uxutil', 'form', 'table', 'dataadapter', 'gridpanel', 'bloodbreqtypeTable', 'breqtypeForm'], function() {
	"use strict";

	var form = layui.form;
	var bloodbreqtypeTable = layui.bloodbreqtypeTable;
	var breqtypeForm = layui.breqtypeForm;

});