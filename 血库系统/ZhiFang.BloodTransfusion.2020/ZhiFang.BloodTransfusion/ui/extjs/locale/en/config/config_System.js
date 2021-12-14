/**
 * 系统参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};
//提示信息
JcallShell.Msg = JcallShell.Msg || {};
JcallShell.Msg.ALERT_TITLE = 'Prompt information';
JcallShell.Msg.WARNING_TITLE = 'Warning information';
JcallShell.Msg.ERROR_TITLE = 'Error message';
JcallShell.Msg.CONFIRM_TEXT = 'Confirmation message';
JcallShell.Msg.DEL_INFO = 'Are you sure you want to delete it?';
JcallShell.Msg.OVERWRITE = ' need to rewrite';
//服务交互
JcallShell.Server = JcallShell.Server || {};
JcallShell.Server.LOADING_TEXT = 'Loading...';
JcallShell.Server.SAVE_TEXT = 'Daving...';
JcallShell.Server.DEL_TEXT = 'Deleting...';
JcallShell.Server.NO_DATA = 'No data found';
//服务交互-状态信息
JcallShell.Server = JcallShell.Server || {};
JcallShell.Server.Status = JcallShell.Server.Status || {};
JcallShell.Server.Status.ERROR_404 = 'Could not find address';
JcallShell.Server.Status.ERROR_505 = 'Server error';
JcallShell.Server.Status.ERROR_UNDEFINED = 'Undefined error';
JcallShell.Server.Status.ERROR_TIMEOUT = 'Request timeout';
JcallShell.Server.Status.ERROR_UNIQUE_KEY = 'Violation of the only key constraint';
//字符串
JcallShell.String = JcallShell.String || {};
JcallShell.String.PARSEERROR = 'Parse error';
//全局预定义变量
JcallShell.All = JcallShell.All || {};
JcallShell.All.ALL = 'All';
JcallShell.All.TRUE = 'True';
JcallShell.All.FALSE = 'False';
JcallShell.All.ADD = 'Add';
JcallShell.All.EDIT = 'Edit';
JcallShell.All.SHOW = 'Show';
JcallShell.All.CHECK_ONE_RECORD = 'Please select a row of data to operate';
JcallShell.All.CHECK_MORE_RECORD = 'Please select data for operation';
JcallShell.All.SUCCESS_TEXT = 'Success';
JcallShell.All.FAILURE_TEXT = 'FAILURE';
JcallShell.All.JSON_ERROR = 'JSON data parsing error';

(function() {
	window.JShell = JcallShell;
})();