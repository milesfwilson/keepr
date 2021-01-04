import swal from 'sweetalert'

class NotificationService {
  hello() {
    swal('Hello world!')
  }
}

export const notificationService = new NotificationService()
