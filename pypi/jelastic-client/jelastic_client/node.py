from typing import NamedTuple


class Node(NamedTuple):
    id: int
    int_ip: str
    node_group: str
    node_type: str
    url: str
    display_name: str = None


Nodes = list[Node]
