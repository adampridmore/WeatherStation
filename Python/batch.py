import unittest


class Batcher:
    def __init__(self, operation, batch_size=100):
        self.operation = operation
        self.batch_size = batch_size
        self.current_batch = []

    def add_to_batch(self, item):
        self.current_batch.append(item)
        if len(self.current_batch) >= self.batch_size:
            self.finished()

    def finished(self):
        self.operation(self.current_batch)
        self.current_batch = []


class TestBatcher(unittest.TestCase):
    def __init__(self, v):
        super().__init__(v)
        self.batches = []

    def operation(self, items):
        self.batches.append(items)

    def test_basic_batching(self):
        batcher = Batcher(self.operation)
        batcher.add_to_batch(1)
        batcher.add_to_batch(2)
        batcher.finished()
        self.assertEqual(len(self.batches), 1)
        self.assertEqual(self.batches[0][0], 1)
        self.assertEqual(self.batches[0][1], 2)

    def test_basic_batching_for_small_batch(self):
        batcher = Batcher(self.operation, 2)
        batcher.add_to_batch(1)
        batcher.add_to_batch(2)
        batcher.add_to_batch(3)
        batcher.finished()
        self.assertEqual(len(self.batches), 2)
        self.assertEqual(self.batches[0][0], 1)
        self.assertEqual(self.batches[0][1], 2)
        self.assertEqual(self.batches[1][0], 3)

if __name__ == '__main__':
    unittest.main()
