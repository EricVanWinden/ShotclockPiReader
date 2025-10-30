import time
import requests


class RestApiReader:

    @staticmethod
    def get_message(uri, timeout=10):
        """
        Synchronous GET request.
        :param uri: The URI (e.g. http://192.168.1.102:8080/time)
        :param timeout: Timeout in seconds.
        :return: Response body as string.
        """
        response = requests.get(uri, timeout=timeout)
        response.raise_for_status()
        return response.text

    @staticmethod
    def post_message(uri, uuid, message, timeout=10):
        """
        Synchronous POST request.
        :param uri: The URI (e.g. https://livescore.easingyou.com/api/shotclock)
        :param uuid: The UUID (e.g. d19c485f-98a9-4cbb-853d-6ff68d21a050 for TOP)
        :param message: The message to send.
        :param timeout: Timeout in seconds.
        :return: True if successful, False otherwise.
        """
        final_uri = f"{uri.rstrip('/')}/{uuid}"
        try:
            response = requests.post(final_uri, data=message, headers={'Content-Type': 'text/plain'}, timeout=timeout)
            return response.ok
        except Exception as e:
            print(f"Exception during post_message: {e}")
            return False


uri = 'http://192.168.1.102:8080/time'
reader = RestApiReader()
while (True):
    msg = reader.get_message(uri)
    print(msg)
    time.sleep(0.05)
