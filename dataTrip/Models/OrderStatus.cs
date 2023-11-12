﻿namespace dataTrip.Models
{
    public enum OrderStatus
    {
        WaitingForPayment, // กำลังรอการชำระเงิน
        PendingApproval, // รอการอนุมัติ
        SuccessfulPayment // ชำระเงินสำเร็จ
    }
}
