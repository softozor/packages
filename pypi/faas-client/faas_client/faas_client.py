import os.path

import sh as sh


def build_env_options(env):
    return [f'-e {key}={value}' for (key, value) in env.items()]


class FaasClient:
    def __init__(self, gateway_url, gateway_port, username, password):
        self.__cli = sh.Command('faas-cli')
        self.endpoint = f'{gateway_url}:{gateway_port}'
        self.__username = username
        self.__password = password
        self.__file_out = open('/tmp/output', 'a')

    def login(self):
        result = self.__cli('login',
                            '-g', self.endpoint,
                            '--username', self.__username,
                            '--password', self.__password,
                            _out=self.__file_out)
        return result.exit_code

    def build(self, path_to_faas_configuration, function_name):
        configuration_filename = os.path.basename(path_to_faas_configuration)
        result = self.__cli(
            'build',
            '-f', configuration_filename,
            '--filter', function_name,
            _cwd=os.path.dirname(path_to_faas_configuration))
        return result.exit_code

    def push(self, path_to_faas_configuration, function_name):
        configuration_filename = os.path.basename(path_to_faas_configuration)
        result = self.__cli(
            'push',
            '-f', configuration_filename,
            '--filter', function_name,
            _cwd=os.path.dirname(path_to_faas_configuration))
        return result.exit_code

    def deploy(self, path_to_faas_configuration, function_name, env={}):
        env_options = build_env_options(env)
        result = self.__cli(
            'deploy',
            '-f', path_to_faas_configuration,
            '--filter', function_name,
            '-g', self.endpoint,
            ' '.join(env_options),
            _out=self.__file_out)
        return result.exit_code


class FaasClientFactory:
    def __init__(self, gateway_port):
        self.__gateway_port = gateway_port

    def create(self, gateway_url, username, password):
        return FaasClient(
            gateway_url,
            self.__gateway_port,
            username,
            password)
