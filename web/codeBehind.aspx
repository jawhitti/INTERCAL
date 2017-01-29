<%@ Page Language="C#" Inherits="hello" %>

<%@Import Namespace="INTERCAL.Runtime" %>

<%

IExecutionContext ctx = ExecutionContext.CreateExecutionContext();
(ctx as ExecutionContext).Output = Response.OutputStream;

DO_0(ref ctx);

%>