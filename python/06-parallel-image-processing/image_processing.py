from PIL import Image
from multiprocessing import Pool
import os
import time
import shutil

directory = "imgs200"

def process_image(name):
    path = os.path.join(directory, name)
    img = Image.open(path)
    img = img.convert("L").rotate(-90).resize((800,600), Image.Resampling.LANCZOS)
    out_name = f"processed/out_{name}"
    img.save(out_name)

def unparalleled_processing(images):
    if os.path.exists("processed"):
        shutil.rmtree("processed")
        os.mkdir("processed")
    start = time.time()
    for img in images:
        process_image(img)
    end = time.time()
    print((end - start), " секунд на обработку последовательно\n")

def parallel_processing(images):
    if os.path.exists("processed"):
        shutil.rmtree("processed")
        os.mkdir("processed")
    start = time.time()
    with Pool() as pool:
        pool.map(process_image, images)
    end = time.time()
    print((end - start), " секунд на обработку параллельно")

def main():
    if not os.path.exists("processed"):
        os.mkdir("processed")
    images = []
    for name in os.listdir(directory):
        images.append(name)
    print(len(images), "изображений в папке\n")
    parallel_processing(images)
    unparalleled_processing(images)


if __name__ == "__main__":
    main()